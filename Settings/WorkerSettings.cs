namespace TaxService.Settings
{
    public class WorkerSettings
    {
        public int ManagerDelay { get; set; }
        public int Delay { get; set; }
        public int? InquiryDelay { get; set; }
        public bool EnableStartupTest { get; set; }
        public int? BulkLimit { get; set; }
        public int? FileId { get; set; }
        public int? TenantId { get; set; }
        public int? IsProd { get; set; }

    }
}
