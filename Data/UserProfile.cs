using TaxService.Data.Enums;

namespace TaxService.Data
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FullName { get; set; }
        public string UserName{ get; set; }
        public EnvironmentType EnvironmentType { get; set; }
        public Company Company { get; set; }
       
    }
}
