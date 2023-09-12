using TaxService.Data.Enums;

namespace TaxService.Data
{
    public class InvoiceResult
    {
        public long Id { get; set; }
        public int ArchivedInvoiceId { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public ErrorLevel ErrorLevel { get; set; }
        public ArchivedInvoice ArchivedInvoice { get; set; }
    }
}
