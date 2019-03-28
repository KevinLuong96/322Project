using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
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
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet]
        [Route("whoami")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public ActionResult<User> GetUserFromToken()
        {
            string username = "";
            var claims = HttpContext.User.Claims;
            foreach (var claim in claims)
            {
                if (claim.Type == "Username")
                {
                    username = claim.Value;
                    break;
                }
            }

            if (username == "")
            {
                return BadRequest();
            }
            return Ok(this._userService.GetUserByName(username));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(User user)
        {
            user.Username = user.Username.ToLower();
            if (_context.Users.Where(u => u.Username == user.Username).FirstOrDefault() != null)
            {
                return BadRequest("User with this email already exists");
            }

            user = await this._userService.CreateUser(user);
            return CreatedAtAction("Register", new { id = user.Id }, user);
        }
    }
}