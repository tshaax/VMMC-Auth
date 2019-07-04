using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.Db
{
    public partial class Tokens
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Value { get; set; }
        public int Type { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Users User { get; set; }
    }
}
