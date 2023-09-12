using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Collections.Concurrent;
using TaxCollectData.Library.Abstraction;
using TaxCollectData.Library.Dto.Config;
using TaxCollectData.Library.Dto.Properties;
using TaxCollectData.Library.Enums;
using TaxCollectData.Library.Exceptions;
using TaxCollectData.Library.Extensions;
using TaxService.Data;
using TaxService.Data.Enums;
using TaxService.Settings;

namespace TaxService.Worker;

public abstract class ServiceBase : BackgroundService
{
    protected readonly ServiceSettings _settings;
    protected int _tenantId;
    protected readonly TaxDbContext _dbContext;
    protected ConcurrentDictionary<int, IServiceProvider> ServiceProviders = new();

    protected ServiceBase(
        ServiceSettings settings,
        int tenantId,
        ITaxDbContextFactory dbContextFactory)
    {
        _settings = settings;
        _dbContext = dbContextFactory.CreateDbContext();
        _tenantId = tenantId;
    }

    protected abstract Task<bool> DoServiceAsync(int tenantId);
    protected abstract void RemoveFromDictionary(int tenantId);
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Logger.Verbose("service worker started at: {time}\r\nTenantId {tenantId}", DateTime.Now, _tenantId);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var isCompleted = await DoServiceAsync(_tenantId);
                if (isCompleted)
                {
                    RemoveFromDictionary(_tenantId);
                    break;
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Service failed with Error:{ex.Message}");
            }

            Log.Logger.Verbose("SendInvoice Service is sleeping for {delay} milisecond", _settings.WorkerSettings.Delay);
            await Task.Delay(_settings.WorkerSettings.Delay, stoppingToken);
        }
    }

    protected async Task<IServiceProvider> GetTaxServiceProviderAsync(int companyProfileId, bool forceRenewToken = false)
    {
        IServiceProvider? serviceProvider;
        if (!forceRenewToken)
        {
            serviceProvider = ServiceProviders.Where(x => x.Key == companyProfileId).Select(x => x.Value).FirstOrDefault();
            if (serviceProvider != null)
                return serviceProvider;
        }

        var services = new ServiceCollection();
        serviceProvider = services.BuildServiceProvider();

        var clientInfo = await _dbContext.CompanyProfiles.Where(x => x.Id == companyProfileId).Select(x => new
        {
            ClientId = x.UniqueTaxMemoryId,
            x.PublicKey,
            x.PrivateKey,
            x.EnvironmentType
        }).FirstOrDefaultAsync();

        var url = _settings.TaxApiSettings.ProdUrl;
        if (clientInfo.EnvironmentType == EnvironmentType.Test)
            url = _settings.TaxApiSettings.TestUrl;

        services.AddTaxApi(
                url,
                clientInfo.ClientId,
                new NormalProperties(ClientType.SELF_TSP),
                new SignatoryConfig(clientInfo.PrivateKey, null),
                null,
                new EncryptionConfig(clientInfo.PublicKey, _settings.TaxApiSettings.EncryptionKeyId));

        serviceProvider = services.BuildServiceProvider();

        var taxApis = serviceProvider.GetService<ITaxApis>() ?? throw new NotInitializedException("ITaxApis");

        await taxApis.GetServerInformationAsync();
        await taxApis.RequestTokenAsync();

        ServiceProviders.TryAdd(companyProfileId, serviceProvider);

        return serviceProvider;
    }

}

public class ClientInfo
{
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
    public bool IsProd { get; set; }
}
