using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _322Api.Models;
using _322Api.Services;

namespace _322Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public UserService _userService;

        public User GetUserFromClaims()
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
                return null;
            }
            return this._userService.GetUserByName(username);

        }
    }
}
