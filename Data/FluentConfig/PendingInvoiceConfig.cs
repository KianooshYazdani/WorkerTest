using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxService.Data.FluentConfig
{
    public class PendingInvoiceConfig : IEntityTypeConfiguration<PendingInvoice>
    {
        public void Configure(EntityTypeBuilder<PendingInvoice> builder)
        {
            builder.ToTable("PendingInvoices");
            builder.Property(x => x.Adis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Bros).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Cap).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Cfee).HasColumnType("decimal(23,4)");
            builder.Property(x => x.Consfee).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Cop).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Dis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Fee).HasColumnType("decimal(27,8)");
            builder.Property(x => x.Insp).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Odam).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Odr).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Olam).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Olr).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Prdis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Spro).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tadis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tax17).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tbill).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tcpbs).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tdis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Todam).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tprdis).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tsstam).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tvam).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Tvop).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Vam).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Vra).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Am).HasColumnType("decimal(22,8)");
            builder.Property(x => x.Exr).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Vop).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Nw).HasColumnType("decimal(25,8)");
            builder.Property(x => x.Tonw).HasColumnType("decimal(25,8)");
            builder.Property(x => x.Tocv).HasColumnType("decimal(20,4)");
            builder.Property(x => x.Torv).HasColumnType("decimal(21,2)");
            builder.Property(x => x.Sscv).HasColumnType("decimal(20,4)");
            builder.Property(x => x.Ssrv).HasColumnType("decimal(21,2)");

            builder.HasOne(x => x.FileHistory)
                .WithMany(y => y.PendingInvoices)
                .HasForeignKey(x => x.FileHistoryId);
        }
    }
}
