using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.Db
{
    public partial class Partners
    {
        public Partners()
        {
            ServiceProviders = new HashSet<ServiceProviders>();
        }

        public int PartnerId { get; set; }
        public string Name { get; set; }
        public int? FunderId { get; set; }
        [JsonIgnore]
        public DateTime? LastModified { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public Funders Funder { get; set; }
        public ICollection<ServiceProviders> ServiceProviders { get; set; }
    }
}
