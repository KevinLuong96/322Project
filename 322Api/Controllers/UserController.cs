using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.IdentityModel;
using System.Security.Cryptography;
using System.Security.Claims;
using _322Api.Models;
using _322Api.Services;

namespace _322Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserService _userService;

        public UserController(DatabaseContext context)
        {
            _context = context;
            this._userService = new UserService(context);
            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User
                {
                    Username = "test@testing.com",
                    Password = this._userService.HashPassword("supersecretstring"),
                    Role = Roles.Admin,
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet]
        [Route("whoami")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public ActionResult DescribeUser()
        {
            var claims = HttpContext.User.Claims;
            List<string> result = new List<string>();
            foreach (var claim in claims)
            {
                result.Add(claim.ToString());
            }

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(User user)
        {
            if (_context.Users.Where(u => u.Username == user.Username).FirstOrDefault() != null)
            {
                return BadRequest("User with this email already exists");
            }

            user = await this._userService.CreateUser(user);
            return CreatedAtAction("Register", new { id = user.Id }, user);
        }
    }
}