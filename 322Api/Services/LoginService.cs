using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
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

            if (dbUser == null) { return false; }

            //Convert hashed password into byte array
            byte[] hashBytes = Convert.FromBase64String(dbUser.Password);
            //Compute hash on user provided password
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //Compare hashed value to original
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }
}
