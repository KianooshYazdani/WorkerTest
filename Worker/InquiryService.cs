using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TaxCollectData.Library.Abstraction;
using TaxService.Data;
using TaxService.DTO.Invoice;
using TaxService.Resources;

namespace TaxService.Worker;

public class InquiryService : ServiceBase
{
    private TaxDbContext DbContext { get; }
    private int BulkLimit { get; }
    private int InquiryDelay { get; }

    public InquiryService(
        Settings.ServiceSettings settings,
        int tenantId,
        ITaxDbContextFactory dbContextFactory) : base(settings, tenantId, dbContextFactory)
    {
        DbContext = dbContextFactory.CreateDbContext();
        BulkLimit = settings.WorkerSettings.BulkLimit ?? 100;
        InquiryDelay = settings.WorkerSettings.InquiryDelay ?? 60000;
    }

    protected override async Task<bool> DoServiceAsync(int tenantId)
    {
        Log.Logger.Debug("Begin InquiryService at: {time}", DateTime.Now);

        var pendingList = await DbContext.PendingInvoices
            .Where(x => x.FileHistory.CompanyProfile.CompanyId == tenantId &&
                        x.ReferenceNumber != null &&
                        x.Result == 0)
            .GroupBy(x => x.Inno)
            .Select(g => new
            {
                g.Key,
                g.First().ReferenceNumber,
                g.First().FileHistory.CompanyProfileId,
                InvoiceList = g.ToList()
            })
            .ToListAsync();

        if (!pendingList.Any())
            return true;

        Log.Logger.Debug("InquiryService; pendingList keys: {keys}", string.Join(",", pendingList.Select(x => x.Key).ToList()));
        var count = 0;
        foreach (var invoice in pendingList)
        {
            try
            {
                var refNumber = invoice.ReferenceNumber;
                Log.Logger.Debug("InquiryService; refNumber = {refNumber}", refNumber);

                if (refNumber != null)
                {
                    if (Guid.TryParse(refNumber, out var newGuid))
                    {
                        var serviceProvider = await GetTaxServiceProviderAsync(invoice.CompanyProfileId);
                        var taxApi = serviceProvider.GetService<ITaxApis>();

                        var result = await CheckInvoiceAsync(refNumber, taxApi);
                        if (!result.Contains(ExceptionMessage.EmptyResult))
                        {
                            Log.Logger.Debug("InquiryService; Get Result ");
                            foreach (var invoiceItem in invoice.InvoiceList)
                            {
                                invoiceItem.Result = result.Contains("SUCCESS") ? 1 : -1;
                                invoiceItem.ResultDate = DateTime.Now;
                                invoiceItem.ResultDetail = result;
                                DbContext.Update(invoiceItem);
                                count++;
                            }
                        }
                        else
                        {
                            Log.Logger.Error($"EmptyResponse for referenceNumber: {refNumber}");
                        }
                    }
                }
                if (count > 0 && count % BulkLimit == 0)
                {
                    Log.Logger.Debug("InquiryService; count: {Count}", count);
                    await DbContext.SaveChangesAsync();
                    Log.Logger.Debug("InquiryService; changes Saved");
                }

                Thread.Sleep(InquiryDelay);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"InquiryService Error: {ex.Message}");
            }
        }

        try
        {
            if (count % BulkLimit > 0)
            {
                Log.Logger.Debug("InquiryService; total count: {count}", count);
                await DbContext.SaveChangesAsync();
                Log.Logger.Debug("InquiryService; All changes Saved");
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error($"InquiryService save Error: {ex.Message}");
        }

        Log.Logger.Debug("END of InquiryService");

        return false;
    }

    private static async Task<string> CheckInvoiceAsync(string referenceNumber, ITaxApis taxApi)
    {
        var results = await taxApi.InquiryByReferenceIdAsync(new() { referenceNumber });
        var result = results?.FirstOrDefault();
        if (result != null && result.Data != null)
            return "status: \"" + result.Status + "\", " + result.Data.ToString();
        await taxApi.RequestTokenAsync();
        return $"{ExceptionMessage.EmptyResult} Ref Id: {referenceNumber}";
    }

    protected override void RemoveFromDictionary(int tenantId)
    {
        ServicesDictionary.InquiryServices.TryRemove(tenantId, out _);
    }

}
