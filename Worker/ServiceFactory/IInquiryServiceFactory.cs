using Microsoft.Extensions.DependencyInjection;
using TaxService.Data;

namespace TaxService.Worker.ServiceFactory;

public interface IInquiryServiceFactory
{
    InquiryService Create(
        Settings.ServiceSettings settings,
        int tenantId,
        ITaxDbContextFactory dbContextFactory);
}

