using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text.Json;
using TaxService.Data;
using TaxService.Data.Enums;
using TaxService.DTO.Invoice;


namespace TaxService.Worker;

public class FileHistoryService : BackgroundService
{
    private TaxDbContext DbContext { get; }
    private readonly ITaxDbContextFactory _dbContextFactory;
    private int BulkLimit { get; }

    public FileHistoryService(
        Settings.ServiceSettings settings,
        ITaxDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        DbContext = _dbContextFactory.CreateDbContext();
        BulkLimit = settings.WorkerSettings.BulkLimit ?? 100;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var completedInvoices = await DbContext.PendingInvoices
                .Where(x => x.ReferenceNumber != null && x.Result != 0)
                .OrderBy(x => x.Inno).ThenBy(x => x.Id)
                .ToListAsync(cancellationToken: stoppingToken);

            var inno = string.Empty;
            var archivedInvoice = new ArchivedInvoice();
            var totalCount = 0;
            var idsToDelete = new List<int>();
            foreach (var invoice in completedInvoices)
            {
                var tx = await DbContext.Database.BeginTransactionAsync(stoppingToken);
                try
                {
                    if (!inno.Equals(invoice.Inno, StringComparison.OrdinalIgnoreCase))
                    {
                        Log.Logger.Debug("FileHistoryService; inno: {inno}", inno);
                        Log.Logger.Debug("FileHistoryService; CreateArchivedInvoice");

                        var invoiceIsExist = await DbContext.ArchivedInvoices.AnyAsync(x =>
                            x.FileHistoryId == invoice.FileHistoryId && x.Inno == invoice.Inno, cancellationToken: stoppingToken);
                        if (invoiceIsExist)
                            Log.Logger.Error($"DuplicateInvoice for Save in Archived found with: inno:{invoice.Inno} " +
                                             $"\r\n fileHistoryId: {invoice.FileHistoryId}" +
                                             $"\r\n referenceNumber: {invoice.ReferenceNumber}");
                        else
                        {
                            archivedInvoice = CreateArchivedInvoice(invoice);
                            await DbContext.ArchivedInvoices.AddAsync(archivedInvoice, stoppingToken);
                            inno = invoice.Inno;

                            Log.Logger.Debug("FileHistoryService; inno: {inno}", inno);
                            Log.Logger.Debug("FileHistoryService; totalCount: {totalCount}", totalCount);

                            totalCount++;
                            if (invoice.Result == -1)
                            {
                                try
                                {
                                    Log.Logger.Debug("FileHistoryService; Result was failed");

                                    var statusResultModel = GetStatusResultModel(invoice);
                                    if (statusResultModel?.error != null)
                                    {
                                        Log.Logger.Debug("FileHistoryService; Result HasError");

                                        foreach (var errorModel in statusResultModel.error)
                                        {
                                            var invoiceResult = CreateInvoiceResult(errorModel, archivedInvoice,
                                                ErrorLevel.Error);
                                            await DbContext.InvoiceResults.AddAsync(invoiceResult, stoppingToken);
                                        }
                                    }

                                    if (statusResultModel?.warning != null)
                                        foreach (var errorModel in statusResultModel.warning)
                                        {
                                            var invoiceResult = CreateInvoiceResult(errorModel, archivedInvoice,
                                                ErrorLevel.Warning);
                                            await DbContext.InvoiceResults.AddAsync(invoiceResult, stoppingToken);
                                        }
                                }
                                catch (Exception ex)
                                {
                                    Log.Logger.Error($"FileHistoryService| Save ResultDetail Failed: {ex.Message}");
                                }
                            }
                        }
                    }

                    var archivedInvoiceDetail = CreateArchivedDetailInvoice(invoice, archivedInvoice);
                    await DbContext.ArchivedDetailInvoices.AddAsync(archivedInvoiceDetail, stoppingToken);
                    idsToDelete.Add(invoice.Id);

                    if (totalCount > 0 && totalCount % BulkLimit == 0)
                    {
                        await DbContext.SaveChangesAsync(stoppingToken);
                        var listToDelete = completedInvoices.Where(x => idsToDelete.Contains(x.Id)).ToList();
                        await DbContext.BulkDeleteAsync(listToDelete, cancellationToken: stoppingToken);
                    }

                    await tx.CommitAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    idsToDelete.Clear();
                    Log.Logger.Error($"Save ResultDetail Failed: {ex.Message}");
                    await tx.RollbackAsync(stoppingToken);
                }
            }

            try
            {
                if (totalCount % BulkLimit > 0)
                    await DbContext.SaveChangesAsync(stoppingToken);

                if (completedInvoices.Any())
                {
                    var listToDelete = completedInvoices.Where(x => idsToDelete.Contains(x.Id)).ToList();
                    await DbContext.BulkDeleteAsync(listToDelete, cancellationToken: stoppingToken);
                }

                var tx = await DbContext.Database.BeginTransactionAsync(stoppingToken);
                try
                {
                    await DbContext.FileHistories.Where(x => x.Status == StatusType.Pending &&
                                                             !x.PendingInvoices.Any() &&
                                                             x.ArchivedInvoices.All(y => y.Result == 1))
                        .BatchUpdateAsync(x => new FileHistory { Status = StatusType.Succeed },
                            cancellationToken: stoppingToken);

                    await DbContext.FileHistories.Where(x => x.Status == StatusType.Pending &&
                                                             !x.PendingInvoices.Any() &&
                                                             x.ArchivedInvoices.All(y => y.Result == -1))
                        .BatchUpdateAsync(x => new FileHistory { Status = StatusType.Failed },
                            cancellationToken: stoppingToken);

                    await DbContext.FileHistories.Where(x => x.Status == StatusType.Pending && !x.PendingInvoices.Any())
                        .BatchUpdateAsync(x => new FileHistory { Status = StatusType.Defective },
                            cancellationToken: stoppingToken);
                    await tx.CommitAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    await tx.RollbackAsync(stoppingToken);
                }

                // Second task of this process to change invoce status from Sending to pending
                await DbContext.FileHistories.Where(x => x.Status == StatusType.Sending &&
                                                         x.PendingInvoices.All(y =>
                                                             y.ReferenceNumber != null && y.Result == 0))
                    .BatchUpdateAsync(x => new FileHistory { Status = StatusType.Pending },
                        cancellationToken: stoppingToken);

            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Save ResultDetail for totalCount {totalCount} Failed: {ex.Message}");
            }

        }
    }

    private static InvoiceResult CreateInvoiceResult(
        ErrorModel errorModel,
        ArchivedInvoice archivedInvoice,
        ErrorLevel errorLevel)
    {
        return new InvoiceResult()
        {
            ArchivedInvoice = archivedInvoice,
            Code = errorModel.code,
            ErrorLevel = errorLevel,
            Message = errorModel.message
        };
    }

    private static InvoiceStatusResultModel? GetStatusResultModel(PendingInvoice invoice)
    {
        var commaIndex = invoice.ResultDetail?.IndexOfAny(new char[] { ',' }, 0);
        if (commaIndex == -1)
            return new InvoiceStatusResultModel()
            {
                error = new List<ErrorModel>() { new ErrorModel() { code = "0", message = invoice.ResultDetail } }
            };

        var stringToDelete = invoice.ResultDetail?[..(commaIndex.GetValueOrDefault() + 1)];
        var finalResult = invoice.ResultDetail.Replace(stringToDelete, string.Empty);
        finalResult = finalResult.TrimStart();
        var statusResultModel = JsonSerializer.Deserialize<InvoiceStatusResultModel>(finalResult);
        return statusResultModel;
    }

    private static ArchivedInvoice CreateArchivedInvoice(PendingInvoice model)
    {
        return new ArchivedInvoice()
        {
            Inno = model.Inno,
            FileHistoryId = model.FileHistoryId,
            Indatim = model.Indatim,
            Tins = model.Tins,
            Bbc = model.Bbc,
            Sbc = model.Sbc,
            Bid = model.Bid,
            Billid = model.Billid,
            Bpc = model.Bpc,
            Bpn = model.Bpn,
            Cap = model.Cap,
            Crn = model.Crn,
            Ft = model.Ft,
            Inp = (InvoicePattern)model.Inp,
            SentDate = DateTime.UtcNow,
            Ins = (InvoiceSubject)model.Ins,
            Insp = model.Insp,
            Inty = (InvoiceType)model.Inty,
            Irtaxid = model.Irtaxid,
            Scc = model.Scc,
            Scln = model.Scln,
            Setm = model.Setm,
            Tadis = model.Tadis,
            Tax17 = model.Tax17,
            Tbill = model.Tbill,
            Tdis = model.Tdis,
            Tinb = model.Tinb,
            Tob = model.Tob,
            Todam = model.Todam,
            Tprdis = model.Tprdis,
            Tvam = model.Tvam,
            Tvop = model.Tvop,
            InnoIns = Convert.ToInt64($"{model.Inno}{model.Ins}"),
            SentJsonstr = model.SentJsonstr,
            ReferenceNumber = model.ReferenceNumber,
            Result = model.Result.GetValueOrDefault(),
            ResultDetail = model.ResultDetail,
            ResultDate = model.ResultDate,
            Status = model.Result <= 0 ? StatusType.Failed : StatusType.Succeed,
            Taxid = model.Taxid,
            Indati2m = model.Indati2m,
            Tocv = model.Tocv,
            Torv = model.Torv,
            Tonw = model.Tonw,
            Cdcd = model.Cdcd,
            Cdcn = model.Cdcn,
            LongInno = model.LongInno
        };

    }

    private static ArchivedDetailInvoice CreateArchivedDetailInvoice(
        PendingInvoice model,
        ArchivedInvoice archivedInvoice)
    {
        return new ArchivedDetailInvoice()
        {
            ArchivedInvoice = archivedInvoice,
            Acn = model.Acn,
            Adis = model.Adis,
            Am = model.Am.Value,
            Bros = model.Bros,
            Bsrn = model.Bsrn,
            Cfee = model.Cfee,
            Consfee = model.Consfee,
            Cop = model.Cop,
            Cut = model.Cut,
            Dis = model.Dis,
            Exr = model.Exr,
            Fee = model.Fee,
            Sstid = model.Sstid,
            Sstt = model.Sstt,
            Mu = model.Mu,
            Iinn = model.Iinn,
            Odam = model.Odam.Value,
            Odr = model.Odr.Value,
            Odt = model.Odt,
            Olam = model.Olam,
            Olr = model.Olr,
            Olt = model.Olt,
            Pcn = model.Pcn,
            Pdt = model.Pdt,
            Pid = model.Pid,
            Prdis = model.Prdis,
            Spro = model.Spro,
            Tcpbs = model.Tcpbs,
            Trmn = model.Trmn,
            Trn = model.Trn,
            Tsstam = model.Tsstam,
            Vam = model.Vam,
            Vop = model.Vop,
            Vra = model.Vra,
            Ssrv = model.Ssrv,
            Sscv = model.Sscv,
            Nw = model.Nw,
            Pmt = model.Pmt.GetValueOrDefault(),
            Pv = model.Pv.GetValueOrDefault()
        };

    }

}
