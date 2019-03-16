using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public ActionResult Authenticate(User user)
        {

            var res = Response;
            if (!loginService.Auth(user))
            {
                return BadRequest("Incorrect username or password");
            }
            return Ok("Login Succesful");
        }
    }
}