using Microsoft.Extensions.DependencyInjection;
using TaxService.Data;

namespace TaxService.Worker.ServiceFactory;

public interface IServiceFactory
{
    FileHistoryService Create(IServiceCollection services,
        Settings.ServiceSettings settings,
        int tenantId,
        string clientId,
        ITaxDbContextFactory dbContextFactory);
}

