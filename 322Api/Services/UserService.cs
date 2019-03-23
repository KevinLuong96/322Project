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
            user.Password = HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            salt.CopyTo(hashBytes, 0);
            hash.CopyTo(hashBytes, salt.Length);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
