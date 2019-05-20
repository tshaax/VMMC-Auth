using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using VMMC.Auth.DataAccess.Models;
using VMMC.Auth.DataAccess;
using VMMC.Auth.Web.API.Models;
using System.Text;

namespace VMMC.Auth.Web.API.Controllers
{
    [Route("api/v1/[Controller]")]
    public class TokenController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IConfiguration _Configuration;

        public TokenController(ApplicationDbContext dbContext,
                RoleManager<IdentityRole> roleManager,
                UserManager<ApplicationUser> userManager,
                IConfiguration configuration)
        {
            this._DbContext = dbContext;
            this._RoleManager = roleManager;
            this._UserManager = userManager;
            this._Configuration = configuration;
        }
        public async Task<IActionResult> Jwt([FromBody] TokenRequestViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            switch (model.grant_type)
            {
                case "password":
                    return await GetToken(model);
                default:
                    // not supported - return a HTTP 401 (Unauthorized)
                    return new UnauthorizedResult();
            }
          
        }

        private async Task<IActionResult> GetToken(TokenRequestViewModel model)
        {
            try
            {
                // check if there's an user with the given username
                var user = await this._UserManager.FindByNameAsync(model.username);
                // fallback to support e-mail address instead of username

                if (user == null && model.username.Contains("@"))
                    user = await this._UserManager.FindByEmailAsync(model.username);

                if (user == null || !await this._UserManager.CheckPasswordAsync(user, model.password))
                {
                    // user does not exists or password mismatch
                    return new UnauthorizedResult();
                }
                // username & password matches: create and return the  Jwt token.
                DateTime now = DateTime.UtcNow;
                // add the registered claims for JWT (RFC7519)./ For more info, see  https://tools.ietf.org/html/rfc7519#section-4.1
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,
                    new
                    DateTimeOffset(now).ToUnixTimeSeconds().ToString())
                                        // TODO: add additional claims here
                };
                               
                var tokenExpirationMins = this._Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
                var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._Configuration["Auth:Jwt:Key"]));
                var token = new JwtSecurityToken(issuer: this._Configuration["Auth:Jwt:Issuer"]
                    ,audience: this._Configuration["Auth:Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                signingCredentials: new SigningCredentials(issuerSigningKey,SecurityAlgorithms.HmacSha256));

                var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
                // build & return the response
                var response = new TokenResponseViewModel()
                {
                    token = encodedToken,
                    expiration = tokenExpirationMins
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                return new UnauthorizedResult();
            }
        }
    }
} 