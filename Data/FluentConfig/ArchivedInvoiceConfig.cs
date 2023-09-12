using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxService.Data.FluentConfig
{
    public class ArchivedInvoiceConfig : IEntityTypeConfiguration<ArchivedInvoice>
    {
        public void Configure(EntityTypeBuilder<ArchivedInvoice> builder)
        {
            builder.ToTable("ArchivedInvoices");
            builder.Property(x => x.Cap).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Insp).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tadis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tax17).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tbill).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tdis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Todam).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tprdis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tvam).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tvop).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tonw).HasColumnType("decimal(25,8)");
            builder.Property(x => x.Tocv).HasColumnType("decimal(20,4)");
            builder.Property(x => x.Torv).HasColumnType("decimal(21,2)");

            builder.HasOne(y => y.FileHistory)
                .WithMany(x => x.ArchivedInvoices)
                .HasForeignKey(x => x.FileHistoryId);
        }
    }
}
