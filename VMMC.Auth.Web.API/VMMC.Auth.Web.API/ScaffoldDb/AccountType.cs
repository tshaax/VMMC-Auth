using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.ScaffoldDb
{
    public partial class AccountType
    {
        public AccountType()
        {
            Users = new HashSet<Users>();
        }

        public int TypeId { get; set; }
        public string TypeDescription { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
