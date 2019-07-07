using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.Db
{
    public partial class Funders
    {
        public Funders()
        {
            Partners = new HashSet<Partners>();
        }
        public int FunderId { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public DateTime? LastModified { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        public ICollection<Partners> Partners { get; set; }
    }
}
