namespace TaxService.DTO.Invoice
{
    public class CompanyProfileModel
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public bool IsProd { get; set; }
    }
}
