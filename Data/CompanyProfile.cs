using System.Collections.Generic;
using TaxService.Data.Enums;

namespace TaxService.Data
{
    public class CompanyProfile
    {
        public int Id { get; set; }
        public  int  CompanyId { get; set; }
        public string ProfileName { get; set; }//todo: change to IdentityName
        public string UniqueTaxMemoryId { get; set; }
        public EnvironmentType EnvironmentType { get; set; }
        public string KeyId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public bool IsDefault { get; set; }
        public Company Company { get; set; }
        public List<FileHistory> FileHistories { get; set; }
    }
}
