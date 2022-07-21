using DAL.Context;
using LeadSub.APIConfig;
using LeadSub.Models;
using LeadSub.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LeadSub.Controllers.api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> Token([FromBody]LoginViewModel model)
        {
            User user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            var res = await signInManager.PasswordSignInAsync(user,model.Password,false,false);
            if (res.Succeeded)
            {
                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(new Claim[]
                //    {
                //        new Claim(ClaimTypes.Name, user.Id.ToString())
                //    }),
                //    Expires = DateTime.UtcNow.AddDays(7)
                //};

                var claims = new Claim[] { new Claim(ClaimTypes.Name, user.Id) };

                var timeNow = DateTime.Now;
                var token = new JwtSecurityToken
                (
                    issuer: AuthOptions.ISSUER,
                    audience:AuthOptions.AUDIENCE,
                    notBefore:timeNow,
                    claims: claims,
                    expires:timeNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials:new SigningCredentials(AuthOptions.GetSymetricKey(),SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
                var response = new
                {
                    access_token = encodedJwt,
                    userName = model.Email
                };
                return Json(response);
            }
            return BadRequest();
        }
    }
}
