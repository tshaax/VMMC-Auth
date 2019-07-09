using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.ScaffoldDb
{
    public partial class Funders
    {
        public Funders()
        {
            Partners = new HashSet<Partners>();
            Users = new HashSet<Users>();
        }

        public int FunderId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Partners> Partners { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
