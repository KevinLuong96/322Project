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
            if (!this._loginService.Auth(user))
            {
                return BadRequest("Incorrect username or password");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //One week expiration 
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(10080),
              signingCredentials: creds);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}