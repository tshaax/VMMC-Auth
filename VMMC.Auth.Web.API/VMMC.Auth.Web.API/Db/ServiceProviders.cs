using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.Db
{
    public partial class ServiceProviders
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public int? PartnerId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime DateCreated { get; set; }

        public Partners Partner { get; set; }
    }
}
