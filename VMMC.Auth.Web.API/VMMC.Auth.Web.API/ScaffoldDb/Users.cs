using System;
using System.Collections.Generic;

namespace VMMC.Auth.Web.API.ScaffoldDb
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
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string DisplayName { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? AccountTypeId { get; set; }
        public int? FunderId { get; set; }
        public int? ProviderId { get; set; }
        public int? PartnerId { get; set; }

        public AccountType AccountType { get; set; }
        public Funders Funder { get; set; }
        public Partners Partner { get; set; }
        public ServiceProviders Provider { get; set; }
        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public ICollection<Tokens> Tokens { get; set; }
    }
}
