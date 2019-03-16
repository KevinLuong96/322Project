using System;
using System.Linq;
using System.Threading.Tasks;
using _322Api.Models;

namespace _322Api.Services
{
    public class LoginService
    {
        private readonly UserContext _context;

        public LoginService(UserContext context)
        {
            this._context = context;
        }

        public bool Auth(User user)
        {
            User dbUser;
            dbUser = this._context.Users.Where(u => u.Username == user.Username).SingleOrDefault();
            //dbUser = await this._context.Users.FindAsync(user.Username);
            if (dbUser == null || user.Password != dbUser.Password) { return false; }

            return true;
        }
    }
}
