using System;
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

        public async Task<bool> Auth(User user){
            User dbUser;

            dbUser = await this._context.Users.FindAsync(user.Username);
            if (dbUser == null || user.Password != dbUser.Password) { return false; }

            return true;
        }
    }
}
