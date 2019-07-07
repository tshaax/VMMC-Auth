using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace VMMC.Auth.Web.API.Db
{
    public partial class Users
    {
        public Users()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Tokens = new HashSet<Tokens>();
        }

       
        public string Id { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string NormalizedEmail { get; set; }
        [JsonIgnore]
        public bool EmailConfirmed { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string SecurityStamp { get; set; }
        [JsonIgnore]
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public bool PhoneNumberConfirmed { get; set; }
        [JsonIgnore]
        public bool TwoFactorEnabled { get; set; }
        [JsonIgnore]
        public DateTimeOffset? LockoutEnd { get; set; }
        [JsonIgnore]
        public bool LockoutEnabled { get; set; }
        [JsonIgnore]
        public int AccessFailedCount { get; set; }
        public string DisplayName { get; set; }
        [JsonIgnore]
        public string Notes { get; set; }
        [JsonIgnore]
        public int Type { get; set; }
        [JsonIgnore]
        public int Flags { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime LastModifiedDate { get; set; }
        [JsonIgnore]
        public int? SourceId { get; set; }
        [JsonIgnore]
        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        [JsonIgnore]
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        [JsonIgnore]
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        [JsonIgnore]
        public ICollection<Tokens> Tokens { get; set; }
    }
}
