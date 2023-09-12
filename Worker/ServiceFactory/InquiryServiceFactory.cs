using Microsoft.Extensions.DependencyInjection;
using TaxService.Data;

namespace TaxService.Worker.ServiceFactory;

public class InquiryServiceFactory : IInquiryServiceFactory
{
    public InquiryService Create(
        Settings.ServiceSettings settings,
        int tenantId,
        ITaxDbContextFactory dbContextFactory)
    {
        return new InquiryService(settings, tenantId, dbContextFactory);
    }
}

