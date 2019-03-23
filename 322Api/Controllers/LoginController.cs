using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using _322Api.Models;
using _322Api.Services;

namespace _322Api.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly LoginService _loginService;

        public LoginController(DatabaseContext context, IConfiguration config)
        {
            this._config = config;
            this._loginService = new LoginService(context);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Authenticate(User user)
        {
            var res = Response;
            user.Username = user.Username.ToLower().Trim();
            if (!this._loginService.Auth(user))
            {
                return BadRequest();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //One week expiration 
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Username", user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Audience = _config["Jwt:Issuer"],
                Issuer = _config["Jwt:Issuer"],
                Expires = DateTime.Now.AddMinutes(10080),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return Ok(handler.WriteToken(token));
        }
    }
}