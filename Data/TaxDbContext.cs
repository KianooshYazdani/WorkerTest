using Microsoft.EntityFrameworkCore;
using TaxService.Data.FluentConfig;


namespace TaxService.Data
{
    public class TaxDbContext : DbContext
    {

        public TaxDbContext(DbContextOptions<TaxDbContext> options)
            : base(options)
        {
            // this.Database.SetCommandTimeout(60000000);
        }

        public virtual DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<FileHistory> FileHistories { get; set; }
        public virtual DbSet<PendingInvoice> PendingInvoices { get; set; }
        public virtual DbSet<ArchivedInvoice> ArchivedInvoices { get; set; }
        public virtual DbSet<ArchivedDetailInvoice> ArchivedDetailInvoices { get; set; }
        public virtual DbSet<InvoiceResult> InvoiceResults { get; set; }

        // lookUp Db Sets
        // public virtual DbSet<Lookup> Lookups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
            base.OnModelCreating(modelBuilder);
            var assembly = typeof(CompanyProfileConfig).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);           

        }
    }
}