using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMMC.Auth.Web.API.Models.Metadata
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public UserRoleModel UserRole { get; set; }
    }

 
}
