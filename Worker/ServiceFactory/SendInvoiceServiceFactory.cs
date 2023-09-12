using Microsoft.Extensions.DependencyInjection;
using TaxService.Data;

namespace TaxService.Worker.ServiceFactory;

public class SendInvoiceServiceFactory : ISendInvoiceServiceFactory
{
    public SendInvoiceService Create(
        Settings.ServiceSettings settings,
        int tenantId,
        ITaxDbContextFactory dbContextFactory)
    {
        return new SendInvoiceService(settings, tenantId, dbContextFactory);
    }
}

