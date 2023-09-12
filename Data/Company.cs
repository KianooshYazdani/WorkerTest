using System.Collections.Generic;

namespace TaxService.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string NationalId { get; set; }
        public string? EconomyNumber { get; set; }
        public string CompanyName { get; set; }
        public string? LogoName { get; set; }
        public List<UserProfile> UserProfiles { get; set; }
        public List<CompanyProfile> CompanyProfiles { get; set; }
        //public List<FileHistory> FileHistories { get; set; }

    }
}
 
