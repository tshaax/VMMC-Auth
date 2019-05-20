using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMMC.Auth.Web.API.Models
{
    public class TokenResponseViewModel
    {
        public string token { get; set; }
        public int expiration { get; set; }
    }
}
