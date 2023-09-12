namespace TaxService.DTO.Invoice
{
    public class InvoiceStatusResultModel
    {
        public int? confirmationReferenceId { get; set; }
        public List<ErrorModel> error { get; set; }
        public List<ErrorModel> warning { get; set; }
        public bool success { get; set; }
    }
    public class ErrorModel
    {
        public string code { get; set; }
        public string message { get; set; }
        public string errortype { get; set; }
    }
}
