using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxService.Data.Enums;

namespace TaxService.Data
{
    public class FileHistory
    {
        public int Id { get; set; }
        //public int CompanyId { get; set; }
        public int CompanyProfileId { get; set; }
        public string FileName { get; set; }
        public DateTime SendDate { get; set; }
        public StatusType Status { get; set; }
        public EnvironmentType EnvironmentType { get; set; }
        public int InvoicesCount { get; set; }
        public string MinInno { get; set; }
        public string MaxInno { get; set; }
        //public Company Company{ get; set; }
        public CompanyProfile CompanyProfile{ get; set; }
        public List<PendingInvoice> PendingInvoices{ get; set; }
        public List<ArchivedInvoice> ArchivedInvoices{ get; set; }
    }
}
