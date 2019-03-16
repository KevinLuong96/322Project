using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.IdentityModel;
using System.Security.Cryptography;
using _322Api.Models;
using _322Api.Services;

namespace _322Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Users.Add(new User { Username = "test@testing.com", Password = "supersecretstring" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetTodoItems()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetTodoItem(string username)
        {
            var todoItem = await _context.Users.FindAsync(username);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (_context.Users.Where(u => u.Username == user.Username).FirstOrDefault() != null)
            {
                return BadRequest("User with username alrady exists");
            }

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            salt.CopyTo(hashBytes, 0);
            hash.CopyTo(hashBytes, salt.Length);
            user.Password = Convert.ToBase64String(hashBytes);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { user.Username }, user);
        }
    }
}