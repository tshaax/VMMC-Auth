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
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public Funders Funder { get; set; }
        public ICollection<ServiceProviders> ServiceProviders { get; set; }
    }
}
