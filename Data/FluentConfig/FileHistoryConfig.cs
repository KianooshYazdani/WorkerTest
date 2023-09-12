using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxService.Data.FluentConfig
{
    public class FileHistoryConfig : IEntityTypeConfiguration<FileHistory>
    {
        public void Configure(EntityTypeBuilder<FileHistory> builder)
        {
            builder.ToTable("FileHistories");

            //builder.HasOne(x => x.Company)
            //    .WithMany(y => y.FileHistories)
            //    .HasForeignKey(x => x.CompanyId);
            
            builder.HasOne(x => x.CompanyProfile)
                .WithMany(y => y.FileHistories)
                .HasForeignKey(x => x.CompanyProfileId);
            
        }
    }
}
