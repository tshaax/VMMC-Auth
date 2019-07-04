using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.Db
{
    public partial class Funders
    {
        public Funders()
        {
            Partners = new HashSet<Partners>();
        }

        public int FunderId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public ICollection<Partners> Partners { get; set; }
    }
}
