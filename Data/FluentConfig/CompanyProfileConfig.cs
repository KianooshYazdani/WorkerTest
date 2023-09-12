using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxService.Data.FluentConfig
{
    public class CompanyProfileConfig : IEntityTypeConfiguration<CompanyProfile>
    {
        public void Configure(EntityTypeBuilder<CompanyProfile> builder)
        {
            builder.ToTable("CompanyProfiles");
            
            builder.HasOne(x => x.Company).WithMany(y => y.CompanyProfiles).HasForeignKey(x => x.CompanyId);
        }
    }
}
