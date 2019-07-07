using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VMMC.Auth.DataAccess.Models;
using VMMC.Auth.Web.API.Data;
using VMMC.Auth.Web.API.Db;
using VMMC.Auth.Web.API.Models.Metadata;
using VMMC.Auth.Web.API.Services;

namespace VMMC.Auth.Web.API.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
   
    public class UserController : BaseApiController
    {
        private readonly IGenericRepository<Users> _Repo;

        /// <summary>
        /// Constructor UserController
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        /// <param name="repo"></param>
        public UserController(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager,
                                                UserManager<ApplicationUser> userManager)
            : base(dbContext, roleManager, userManager) {}

        /// <summary>
        /// GetUsers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            return this.Ok( this.DbContext.Set<ApplicationUser>());
        }
        /// <summary>
        /// CreateUser
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel userModel)
        {
           
            return this.Ok(await CreateUsers(userModel));
        }

        private async Task<int> CreateUsers(UserModel userModel)
        {
            DateTime createdDate = DateTime.Now;
            
            string role = userModel.UserRole.RoleName;
            if (!await RoleManager.RoleExistsAsync(role))
            {
                await RoleManager.CreateAsync(new
                IdentityRole(role));
            }
            var user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userModel.Username,
                Email = userModel.Email,
                DisplayName = userModel.DisplayName,
                Type = userModel.UserRole.RoleId,
                CreatedDate = createdDate,   
                
            };

            if (await UserManager.FindByNameAsync(user.UserName) == null)
            {
               
                try
                {
                   var res = await UserManager.CreateAsync(user, userModel.Password);
                    if (res.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user, role);
                    }
                }
                catch (Exception e)
                {

                }
           
                // Remove Lockout and E-Mail confirmation.
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
            }
            return await this.DbContext.SaveChangesAsync();
        }


    }
}