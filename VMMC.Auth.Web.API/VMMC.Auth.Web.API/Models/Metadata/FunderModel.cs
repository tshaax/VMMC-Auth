using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMMC.Auth.Web.API.Models.Metadata
{
    public class FunderModel
    {
        public string Name { get; set; }
        public List<PartnerModel> Partners { get; set; }
    }
}
