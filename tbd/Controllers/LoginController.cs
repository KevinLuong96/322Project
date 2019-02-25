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
    public class LoginController : ControllerBase
    {
        private readonly UserContext _context;
        private LoginService loginService;

        public LoginController(UserContext context)
        {
            _context = context;
            loginService = new LoginService(context);
        }
        [HttpGet]
        public string test()
        {
            return "test";
        }

        [HttpPost]
        public async Task<ActionResult<string>> Authenticate(User user)
        {
            var id = _context.Users.Count();
            if (await loginService.Auth(user))
            {
                return "Token";
            }

            return "Unauth";
        }
    }
}