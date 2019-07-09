using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.ScaffoldDb
{
    public partial class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public AspNetRoles Role { get; set; }
        public Users User { get; set; }
    }
}
