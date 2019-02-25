using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tbd.Models;
using tbd.Services;

namespace tbd.Controllers
{
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
                _context.Users.Add(new User { Username = "Item1" });
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
        public async Task<ActionResult<User>> PostTodoItem(User user)
        {
            //var id = _context.Users.Count();
            if (await _context.Users.FindAsync(user.Username) != null){
                return BadRequest("User with username already exists");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { user.Username }, user);
        }
    }
}