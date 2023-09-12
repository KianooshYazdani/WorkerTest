using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxService.Data.FluentConfig
{
    public class InvoiceResultConfig : IEntityTypeConfiguration<InvoiceResult>
    {
        public void Configure(EntityTypeBuilder<InvoiceResult> builder)
        {
            builder.ToTable("InvoiceResults");

            builder.HasOne(x => x.ArchivedInvoice)
                .WithMany(y => y.InvoiceResults)
                .HasForeignKey(x => x.ArchivedInvoiceId);
        }
    }
}
