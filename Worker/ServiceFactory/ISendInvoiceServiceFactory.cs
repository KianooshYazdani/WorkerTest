using Microsoft.Extensions.DependencyInjection;
using TaxService.Data;

namespace TaxService.Worker.ServiceFactory;

public interface ISendInvoiceServiceFactory
{
    SendInvoiceService Create(
        Settings.ServiceSettings settings,
        int tenantId,
        ITaxDbContextFactory dbContextFactory);
}

