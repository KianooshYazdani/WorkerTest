using Microsoft.Extensions.DependencyInjection;

namespace TaxService.Worker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IServiceCollection other)
        {
            foreach (var descriptor in other)
            {
                services.Add(descriptor);
            }

            return services;
        }
    }
}
