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

        public User GetUserById(int Id)
        {
            return this._context.Users.Find(Id);
        }

        public int GetUserIdByName(string userName)
        {
            User user = this._context.Users.Where(u => u.Username == userName).FirstOrDefault();
            if (user is null)
            {
                return 0;
            }
            return user.Id;
        }

        public User GetUserByName(string username)
        {
            int Id = this.GetUserIdByName(username);
            if (Id == 0)
            {
                return null;
            }
            else
            {
                return this.GetUserById(Id);
            }
        }

        public async Task<User> AddToHistory(User user, string search)
        {
            bool found = false;
            string[] newHistory;
            if (user.History is null)
            {
                user.History = new string[0];
            }
            for (int i = 0; i < user.History.Length; i++)
            {
                if (user.History[i] == search)
                {
                    found = true;
                }
            }
            if (found)
            {
                newHistory = new string[user.History.Length];
            }
            else
            {
                newHistory = new string[user.History.Length + 1];
            }
            newHistory[0] = search;
            int j = 1;
            foreach (string s in user.History)
            {
                if (s != search)
                {
                    newHistory[j] = s;
                    j++;
                }
            }

            user.History = newHistory;
            this._context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;

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
