
namespace TaxService.DTO.Common
{
    public class SendInvoiceOutputModel
    {
        public string ReferenceNumber { get; set; }
        public string SentJsonStr { get; set; }
        public int? StatusCode { get; set; }
    }
}
