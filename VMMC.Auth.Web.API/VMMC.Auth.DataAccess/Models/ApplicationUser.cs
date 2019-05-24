using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VMMC.Auth.DataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public  int UserId { get; set; }
        public string  Email { get; set; }
        public string  DisplayName { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
