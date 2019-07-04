using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.Db
{
    public partial class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }

        public Users User { get; set; }
    }
}
