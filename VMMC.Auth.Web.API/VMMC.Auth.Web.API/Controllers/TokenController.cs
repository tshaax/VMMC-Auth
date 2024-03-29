﻿using System;
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
using VMMC.Auth.Web.API.Data;
using Microsoft.AspNetCore.Cors;
using VMMC.Auth.Web.API.Models.Metadata;

namespace VMMC.Auth.Web.API.Controllers
{
 
    public class TokenController : BaseApiController
    {
        #region Private Members
        #endregion Private Members

        #region Constructor
        public TokenController(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration
            )
            : base(context, roleManager, userManager, configuration)
        {
        }
        #endregion

        /// <summary>
        /// Get Access Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        [HttpPost("Auth")]
        public async Task<IActionResult> Jwt([FromBody]TokenRequestViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null) return new StatusCodeResult(500);

            switch (model.grant_type)
            {
                case "password":
                    return await GetToken(model);
                case "refresh_token":
                    return await RefreshToken(model);
                default:
                    // not supported - return a HTTP 401 (Unauthorized)
                    return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> RefreshToken(TokenRequestViewModel model)
        {
            try
            {
                // check if the received refreshToken exists for the given clientId
                var rt = DbContext.Tokens.FirstOrDefault(t => t.Value == model.refresh_token && t.ClientId == model.client_id);

                if (rt == null)
                {
                    return new UnauthorizedResult();
                }

                var user = await UserManager.FindByIdAsync(rt.UserId);

                if (user == null)
                {
                    return new UnauthorizedResult();
                }

                // generate a new refresh token
                var rtNew = CreateRefreshToken(rt.ClientId, rt.UserId);

                // invalidate the old refresh token (by deleting it)
                DbContext.Tokens.Remove(rt);

                // add the new refresh token
                DbContext.Tokens.Add(rtNew);

                // persist changes in the DB
                DbContext.SaveChanges();

                // create a new access token...
                var response = CreateAccessToken(rtNew.UserId, rtNew.Value);

                // ... and send it to the client
                return Json(response);
            }
            catch
            {
                return new UnauthorizedResult();
            }
        }

        private Token CreateRefreshToken(string clientId, string userId)
        {
            return new Token()
            {
                ClientId = clientId,
                UserId = userId,
                Type = 0,
                Value = Guid.NewGuid().ToString("N"),
                CreatedDate = DateTime.UtcNow
            };
        }

        private TokenResponseViewModel CreateAccessToken(string userId, string refreshToken)
        {
            DateTime now = DateTime.UtcNow;

            // add the registered claims for JWT (RFC7519).
            // For more info, see https://tools.ietf.org/html/rfc7519#section-4.1
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti,
                  Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
                // TODO: add additional claims here
            };

            var tokenExpirationMins = Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["Auth:Jwt:Issuer"],
                audience: Configuration["Auth:Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponseViewModel()
            {
                token = encodedToken,
                expiration = tokenExpirationMins,
                refresh_token = refreshToken
            };
        }

        private async Task<IActionResult> GetToken(TokenRequestViewModel model)
        {
            try
            {
                // check if there's an user with the given username
                var user = await UserManager.FindByNameAsync(model.username);
                // fallback to support e-mail address instead of username
                if (user == null && model.username.Contains("@"))
                    user = await UserManager.FindByEmailAsync(model.username);


                if (user == null || !await UserManager.CheckPasswordAsync(user, model.password))
                {
                    // user does not exists or password mismatch
                    return new UnauthorizedResult();
                }

                // username & password matches: create and return the Jwt token.

                // username & password matches: create the refresh token
                var rt = CreateRefreshToken(model.client_id, user.Id);

                // add the new refresh token to the DB
                DbContext.Tokens.Add(rt);
                DbContext.SaveChanges();

                // create & return the access token
                var t = CreateAccessToken(user.Id, rt.Value);
                return Json(t);
            }
            catch (Exception ex)
            {
                return new UnauthorizedResult();
            }
        }
    }
}