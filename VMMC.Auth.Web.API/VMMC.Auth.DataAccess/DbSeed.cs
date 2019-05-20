using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using VMMC.Auth.DataAccess.Models;

namespace VMMC.Auth.DataAccess
{
    public class DbSeedder
    {
        public static void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.Users.Any()) CreateUsers(dbContext);
        }

        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            DateTime createdDate = new DateTime(2019, 06, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;

            var user_Admin = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "tshaax@gmail.com",
                CreatedDate = createdDate,
                LastModified = lastModifiedDate
             };

            dbContext.Users.Add(user_Admin);
        }
    }
}
