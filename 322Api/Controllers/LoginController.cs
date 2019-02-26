using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _322Api.Models;
using _322Api.Services;

namespace _322Api.Controllers
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