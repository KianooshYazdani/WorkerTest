using AfaghInvoiceManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Text.Json;
using TaxCollectData.Library.Abstraction;
using TaxCollectData.Library.Dto.Content;
using TaxService.Data;
using TaxService.Data.Enums;
using TaxService.DTO.Common;
using TaxService.DTO.Invoice;
using TaxService.Resources;

namespace TaxService.Worker;

public class SendInvoiceService : ServiceBase
{
    private TaxDbContext DbContext { get; }
    private int BulkLimit { get; }

    public SendInvoiceService(
        Settings.ServiceSettings settings,
        int tenantId,
        ITaxDbContextFactory dbContextFactory) : base(settings, tenantId, dbContextFactory)
    {
        DbContext = dbContextFactory.CreateDbContext();
        BulkLimit = settings.WorkerSettings.BulkLimit ?? 100;
    }

    protected override async Task<bool> DoServiceAsync(int tenantId)
    {
        Log.Logger.Verbose("service worker started at: {time}\r\nTenantId {tenantId}", DateTime.Now, tenantId);
        Console.WriteLine($"service worker started at: {DateTime.Now}\r\nTenantId {tenantId}");
        Log.Logger.Debug("START SendInvoice at: {time}", DateTime.Now);

        var pendingList = await DbContext.PendingInvoices
            .Where(x => x.FileHistory.CompanyProfile.CompanyId == tenantId && x.ReferenceNumber == null)
            .Include(x => x.FileHistory)
            .OrderBy(x => x.FileHistory.CompanyProfileId).OrderBy(x => x.FileHistoryId).OrderBy(x => x.Inno)
            .GroupBy(x => new { x.FileHistory.CompanyProfileId, x.FileHistoryId, x.Inno }).Select(g => new
            {
                g.Key.CompanyProfileId,
                g.Key.FileHistoryId,
                g.Key.Inno,
                pList = g.ToList()
            })
            .ToListAsync();

        if (!pendingList.Any())
            return true;

        Log.Logger.Debug("SendInvoice; pendingList Innos: {Innos}", string.Join(",", pendingList.Select(x => x.Inno).ToList()));

        var count = 0;
        var fileId = 0;
        var companyProfile = new CompanyProfileModel();

        foreach (var pendingInvoice in pendingList)
        {
            try
            {
                Log.Logger.Debug("SendInvoice; fileId = {fileId}", fileId);
                //create dtoList

                if (pendingInvoice.FileHistoryId != fileId)
                {
                    companyProfile = await DbContext.FileHistories
                        .Where(x => x.CompanyProfile.CompanyId == tenantId && x.Id == pendingInvoice.FileHistoryId)
                        .Select(x => new CompanyProfileModel()
                        {
                            ClientId = x.CompanyProfile.UniqueTaxMemoryId,
                        })
                        .FirstOrDefaultAsync();

                    fileId = pendingInvoice.FileHistoryId;

                }
                var buyerInfo = pendingInvoice.pList.Select(x => new BuyerInfoModel { Tob = x.Tob, Tinb = x.Tinb, Bid = x.Bid }).FirstOrDefault();

                var bidWithPadding = buyerInfo.Bid;
                if (buyerInfo is { Tob: 1, Bid.Length: < 10 })
                {
                    bidWithPadding = buyerInfo.Bid.PadLeft(10, '0');
                }

                var tinbWithPadding = buyerInfo.Tinb;
                //if (buyerInfo is { Tob: 1, Tinb.Length: < 10 })
                if (buyerInfo.Tob== 1 && buyerInfo.Tinb!=null && buyerInfo.Tinb?.Length< 10 )
                {
                    tinbWithPadding = buyerInfo.Tinb.PadLeft(10, '0');
                }
                var indati2m = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                var invoiceItems = pendingInvoice.pList.Select(x => new AfaghInvoiceManager.DTO.PendingInvoice()
                {
                    Header = new AfaghInvoiceManager.DTO.AfaghInvoiceHeaderDto()
                    {
                        Inno = x.Inno,
                        Ins = x.Ins,
                        Setm = x.Setm,
                        Bbc = x.Bbc,
                        Bid = bidWithPadding,
                        Billid = x.Billid,
                        Bpc = x.Bpc,
                        Bpn = x.Bpn,
                        Cap = x.Cap,
                        Crn = x.Crn,
                        Ft = x.Ft,
                        Indati2m = x.Indati2m ?? indati2m,
                        Indati2mDate = x.Indati2mDate,
                        Indatim = x.Indatim,
                        IndatimDate = x.IndatimDate,
                        Inp = x.Inp,
                        Insp = x.Insp,
                        Inty = x.Inty,
                        Irtaxid = x.Irtaxid,
                        Sbc = x.Sbc,
                        Scc = x.Scc,
                        Scln = x.Scln,
                        Tadis = x.Tadis,
                        Tax17 = x.Tax17,
                        Taxid = x.Taxid,
                        Tbill = x.Tbill,
                        Tdis = x.Tdis,
                        Tinb = tinbWithPadding,
                        Tins = x.Tins,
                        Tob = x.Tob,
                        Todam = x.Todam,
                        Tprdis = x.Tprdis,
                        Tvam = x.Tvam,
                        Tvop = x.Tvop,
                        Cdcd = x.Cdcd,
                        Cdcn = x.Cdcn,
                        Tocv = x.Tocv,
                        Tonw = x.Tonw,
                        Torv = x.Torv
                    },
                    Body = new AfaghInvoiceManager.DTO.AfaghInvoiceBodyDto()
                    {
                        Adis = x.Adis,
                        Am = x.Am,
                        Bros = x.Bros,
                        Bsrn = x.Bsrn,
                        Cfee = x.Cfee,
                        Consfee = x.Consfee,
                        Cop = x.Cop,
                        Cut = x.Cut,
                        Dis = x.Dis,
                        Exr = x.Exr,
                        Fee = x.Fee,
                        Mu = x.Mu,
                        Odam = x.Odam,
                        Odr = x.Odr,
                        Odt = x.Odt,
                        Olam = x.Olam,
                        Olr = x.Olr,
                        Olt = x.Olt,
                        Prdis = x.Prdis,
                        Spro = x.Spro,
                        Sstid = x.Sstid,
                        Sstt = x.Sstt,
                        Tcpbs = x.Tcpbs,
                        Tsstam = x.Tsstam,
                        Vam = x.Vam,
                        Vop = x.Vop,
                        Vra = x.Vra,
                        Sscv = x.Sscv,
                        Ssrv = x.Ssrv,
                        Nw = x.Nw,
                    }
                    //,
                    //Payment = new AfaghInvoiceManager.DTO.AfaghPaymentDto()
                    //{
                    //    Acn = x.Acn,
                    //    Iinn = x.Iinn,
                    //    Pcn = x.Pcn,
                    //    Pdt = x.Pdt,
                    //    PdtDate = x.PdtDate,
                    //    Pid = x.Pid,
                    //    Trmn = x.Trmn,
                    //    Trn = x.Trn,
                    //    Pmt = x.Pmt,
                    //    Pv = x.Pv
                    //}
                }).ToList();
                //call sendInvoice for dtoList

                var serviceProvider = await GetTaxServiceProviderAsync(pendingInvoice.CompanyProfileId);
                var taxApi = serviceProvider.GetService<ITaxApis>();
                var taxIdGenerator = serviceProvider.GetService<ITaxIdGenerator>();

                var result = await SendInvoiceAsync(companyProfile.ClientId, taxApi, taxIdGenerator, invoiceItems);
                Console.WriteLine($"Invoice id: {pendingInvoice.Inno} and clientId: {companyProfile.ClientId} and tenantId:{tenantId}");

                //save referenceNumber
                Log.Logger.Debug("SendInvoice; statusCode= {statusCode}", result.StatusCode);
                if (result.StatusCode == 200)
                {
                    foreach (var invoice in pendingInvoice.pList)
                    {
                        invoice.ReferenceNumber = result.ReferenceNumber;
                        var taxid = FixInvoiceTaxId(invoice, companyProfile.ClientId, taxIdGenerator);
                        invoice.Taxid = taxid;
                        invoice.Indati2m ??= indati2m;
                        invoice.Bid = bidWithPadding;
                        invoice.Tinb = tinbWithPadding;
                        invoice.SentJsonstr = result.SentJsonStr;
                        DbContext.Update(invoice);
                        count++;
                    }
                }
                else
                {
                    serviceProvider = await GetTaxServiceProviderAsync(pendingInvoice.CompanyProfileId, true);

                    result = await SendInvoiceAsync(companyProfile.ClientId, taxApi, taxIdGenerator, invoiceItems);
                    Console.WriteLine($"Invoice id: {pendingInvoice.Inno} and clientId: {companyProfile.ClientId} and tenantId:{tenantId}");

                    //save referenceNumber
                    Log.Logger.Debug("SendInvoice; statusCode= {statusCode}", result.StatusCode);
                    if (result.StatusCode == 200)
                    {
                        foreach (var invoice in pendingInvoice.pList)
                        {
                            invoice.ReferenceNumber = result.ReferenceNumber;
                            var taxid = FixInvoiceTaxId(invoice, companyProfile.ClientId, taxIdGenerator);
                            invoice.Taxid = taxid;
                            invoice.Indati2m ??= indati2m;
                            invoice.Bid = bidWithPadding;
                            invoice.Tinb = tinbWithPadding;
                            invoice.SentJsonstr = result.SentJsonStr;
                            DbContext.Update(invoice);
                            count++;
                        }
                    }
                    else
                    {
                        foreach (var invoice in pendingInvoice.pList)
                        {
                            invoice.SentJsonstr = result.SentJsonStr;
                            DbContext.Update(invoice);
                            count++;
                        }
                    }
                }
                if (count > 0 && count % BulkLimit == 0)
                {
                    Log.Logger.Debug("SendInvoice; count: {Count}", count);
                    await DbContext.SaveChangesAsync();
                    Log.Logger.Debug("SendInvoice; changes Saved");
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Debug("SendInvoice; count: {Count}", count);
                Log.Logger.Error($"SendInvoiceService Error:{ex.Message}");
            }
        }

        try
        {
            if (count % BulkLimit > 0)
            {
                Log.Logger.Debug("SendInvoice; total count: {totalcount}", count);
                await DbContext.SaveChangesAsync();
                Log.Logger.Debug("SendInvoice; All changes Saved");
            }

        }
        catch (Exception ex)
        {
            Log.Logger.Error($"SendInvoiceService SaveChanges Error:{ex.Message}");
        }
        Log.Logger.Debug("END of SendInvoice");

        return false;
    }

    public class BuyerInfoModel
    {
        public int? Tob { get; set; }
        public string? Bid { get; set; }
        public string? Tinb { get; set; }
    }

    private static async Task<SendInvoiceOutputModel> SendInvoiceAsync(
        string clientId,
        ITaxApis taxApi,
        ITaxIdGenerator taxIdGenerator,
        IEnumerable<AfaghInvoiceManager.DTO.PendingInvoice> invoiceItems)
    {
        Log.Logger.Verbose($"SendInvoices");
        try
        {
            foreach (var invoice in invoiceItems)
                FixInvoiceTaxId(clientId, taxIdGenerator, invoice);

            var invoices = new List<InvoiceDto>(invoiceItems.Select(x => x.GetInvoiceDto()));
            var mergedInoice = NetworkUilities.MergeInvoices(invoices);

            invoices = new List<InvoiceDto> { mergedInoice };

            var invoiceItemsJson = JsonSerializer.Serialize(invoices);
            var response = await taxApi.SendInvoicesAsync(invoices, null);

            if (response?.Status != 200) return new SendInvoiceOutputModel()
            {
                ReferenceNumber = string.Format(ExceptionMessage.SendingError, response?.Status),
                SentJsonStr = invoiceItemsJson,
                StatusCode = response?.Status
            };
            if (response.Body == null) return new SendInvoiceOutputModel()
            {
                ReferenceNumber = ExceptionMessage.EmptyResponse,
                SentJsonStr = invoiceItemsJson,
                StatusCode = response?.Status
            };

            var results = response.Body.Result.ToArray();
            return new SendInvoiceOutputModel()
            {
                ReferenceNumber = results[0].ReferenceNumber,
                SentJsonStr = invoiceItemsJson,
                StatusCode = response?.Status
            };
        }
        catch (Exception ex)
        {
            Log.Logger.Error($"Failed to send invoice: {ex.Message}");
            return new SendInvoiceOutputModel()
            {
                ReferenceNumber = ExceptionMessage.SendingError,
                SentJsonStr = string.Empty,
                StatusCode = 0
            };
        }

    }
    private static void FixInvoiceTaxId(string clientId, ITaxIdGenerator taxIdGenerator, AfaghInvoiceManager.DTO.PendingInvoice invoice)
    {
        DateTime dateTimeR = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        try
        {
            dateTimeR = dateTimeR.AddSeconds(invoice.Header.Indatim ?? 0).ToLocalTime();
        }
        catch (ArgumentOutOfRangeException)
        {
            dateTimeR = dateTimeR.AddMilliseconds(invoice.Header.Indatim ?? 0).ToLocalTime();
        }

        var taxId = taxIdGenerator.GenerateTaxId(clientId, Convert.ToInt64(invoice.Header.Inno), dateTimeR);
        invoice.Header.Taxid = taxId;
    }

    private static string FixInvoiceTaxId(PendingInvoice invoice, string clientId, ITaxIdGenerator taxIdGenerator)
    {

        DateTime dateTimeR = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        try
        {
            dateTimeR = dateTimeR.AddSeconds(invoice.Indatim ?? 0).ToLocalTime();
        }
        catch (ArgumentOutOfRangeException)
        {
            dateTimeR = dateTimeR.AddMilliseconds(invoice.Indatim ?? 0).ToLocalTime();
        }

        var taxId = taxIdGenerator.GenerateTaxId(clientId, Convert.ToInt64(invoice.Inno), dateTimeR);
        return taxId;
    }

    protected override void RemoveFromDictionary(int tenantId)
    {
        ServicesDictionary.SendInvoiceServices.TryRemove(tenantId, out _);
    }
}
