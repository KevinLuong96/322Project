using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using _322Api.Models;

namespace _322Api.Services
{
    public class UserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<User> CreateUser(User user)
        {
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

            return user;
        }
    }
}
