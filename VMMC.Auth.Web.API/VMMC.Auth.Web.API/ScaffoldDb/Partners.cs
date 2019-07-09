using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.ScaffoldDb
{
    public partial class Partners
    {
        public Partners()
        {
            ServiceProviders = new HashSet<ServiceProviders>();
            Users = new HashSet<Users>();
        }

        public int PartnerId { get; set; }
        public string Name { get; set; }
        public int? FunderId { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? IsDeleted { get; set; }

        public Funders Funder { get; set; }
        public ICollection<ServiceProviders> ServiceProviders { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
