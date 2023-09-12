using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TaxService.Data
{
    public class TaxDbContextFactory : ITaxDbContextFactory
    {
        private readonly IConfiguration _configuration;

        public TaxDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TaxDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaxDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Tax"));
            return new TaxDbContext(optionsBuilder.Options);
        }
    }
}
