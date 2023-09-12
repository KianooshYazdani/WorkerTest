using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Serilog;
using TaxService.Data;
using TaxService.Data.Enums;
using TaxService.Worker.ServiceFactory;

namespace TaxService.Worker;

public class WorkerManager : BackgroundService
{
    private readonly Settings.ServiceSettings _settings;

    private readonly ISendInvoiceServiceFactory _sendInvoiceServiceFactory;
    private readonly IInquiryServiceFactory _inquiryServiceFactory;
    private readonly ITaxDbContextFactory _dbContextFactory;
    private readonly TaxDbContext _dbContext;

    public WorkerManager(
        ISendInvoiceServiceFactory sendInvoiceServiceFactory,
        IInquiryServiceFactory inquiryServiceFactory,
        Settings.ServiceSettings settings,
        ITaxDbContextFactory dbContextFactory)
    {
        _sendInvoiceServiceFactory = sendInvoiceServiceFactory;
        _inquiryServiceFactory = inquiryServiceFactory;
        _dbContextFactory = dbContextFactory;
        _dbContext = _dbContextFactory.CreateDbContext();
        _settings = settings;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CreateServicesAsync(stoppingToken);

                await Task.Delay(_settings.WorkerSettings.ManagerDelay, stoppingToken);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Service failed with Error:{ex.Message}");
            }
        }

        //Stop and dispose all services when the application is shutting down
        foreach (var service in ServicesDictionary.SendInvoiceServices.Values)
        {
            await service.StopAsync(stoppingToken);
        }
    }

    private async Task CreateServicesAsync(CancellationToken stoppingToken)
    {
        try
        {
            var keys = ServicesDictionary.SendInvoiceServices.Keys;

            var tenantIds = await _dbContext.Companies
                .Where(x => x.CompanyProfiles.Any(y =>
                    y.FileHistories.Any(
                        z => z.CompanyProfile.CompanyId == y.CompanyId && z.Status == StatusType.Sending)))
                .Where(x => !keys.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken: stoppingToken);


            foreach (var tenantId in tenantIds)
            {
                var sendInvoiceService = _sendInvoiceServiceFactory.Create(
                    _settings,
                    tenantId,
                    _dbContextFactory);
                ServicesDictionary.SendInvoiceServices.TryAdd(tenantId, sendInvoiceService);
                await sendInvoiceService.StartAsync(stoppingToken);

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        try
        {
            var keys = ServicesDictionary.InquiryServices.Keys;

            var tenantIds = await _dbContext.Companies
                .Where(x => x.CompanyProfiles.Any(y =>
                    y.FileHistories.Any(
                        z => z.CompanyProfile.CompanyId == y.CompanyId && z.Status == StatusType.Pending)))
                .Where(x => !keys.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken: stoppingToken);

            foreach (var tenantId in tenantIds)
            {
                var inquiryServices = _inquiryServiceFactory.Create(
                    _settings,
                    tenantId,
                    _dbContextFactory);
                ServicesDictionary.InquiryServices.TryAdd(tenantId, inquiryServices);
                await inquiryServices.StartAsync(stoppingToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

}
