using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMMC.Auth.DataAccess.Models;

namespace VMMC.Auth.Web.API.Data
{
    public class DbSeedder
    {
        public static void Seed(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager,
                                                UserManager<ApplicationUser> userManager)
        {
            if (!dbContext.Users.Any())
            {
                CreateUsers(dbContext, roleManager, userManager).GetAwaiter().GetResult();
            }
        }

        private static async Task CreateUsers(ApplicationDbContext dbContext
            , RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            DateTime createdDate = new DateTime(2019, 06, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;

            string role_AppAdmin = "AppAdmin";
            string role_SocialMobilazer = "SocialMobilazer";

            if (!await roleManager.RoleExistsAsync(role_AppAdmin))
            {
                await roleManager.CreateAsync(new
                IdentityRole(role_AppAdmin));
            }
            if (!await roleManager.RoleExistsAsync(role_SocialMobilazer))
            {
                await roleManager.CreateAsync(new
                IdentityRole(role_SocialMobilazer));
            }

            var user_Admin = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "tshaax@gmail.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            var user_Mobilazer = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Mobilazer",
                Email = "tshaax@gmail.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            if (await userManager.FindByNameAsync(user_Admin.UserName) == null)
            {
                await userManager.CreateAsync(user_Admin, "Pass4Admin");
                await userManager.AddToRoleAsync(user_Admin, role_AppAdmin);
                // Remove Lockout and E-Mail confirmation.
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;
            }
            if (await userManager.FindByNameAsync(user_Mobilazer.UserName) == null)
            {
                await userManager.CreateAsync(user_Mobilazer, "Pass4Mobilazer");
                await userManager.AddToRoleAsync(user_Mobilazer, role_SocialMobilazer);
                // Remove Lockout and E-Mail confirmation.
                user_Mobilazer.EmailConfirmed = true;
                user_Mobilazer.LockoutEnabled = false;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
