using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VMMC.Auth.Web.API.Db
{
    public partial class ServiceProviders
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public int? PartnerId { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModified { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public Partners Partner { get; set; }
    }
}
