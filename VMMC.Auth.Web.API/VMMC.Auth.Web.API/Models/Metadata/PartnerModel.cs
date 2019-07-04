using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMMC.Auth.Web.API.Models.Metadata
{
    public class PartnerModel
    {
        public string Name { get; set; }
        public int? FunderId { get; set; }
        public List<ServiceProvidersModel> ServiceProviders { get; set; }
    }
}
