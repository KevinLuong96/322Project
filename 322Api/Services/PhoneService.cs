using System;
using System.Linq;
using _322Api.Models;
using System.Threading.Tasks;

namespace _322Api.Services
{
    public class PhoneService
    {
        private readonly DatabaseContext _context;

        public PhoneService(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<Phone> CreatePhone(string phoneName)
        {
            Phone phone;
            phone = new Phone { Name = phoneName, Score = 0, LastCrawl = DateTime.Now };
            this._context.Phones.Add(phone);
            await this._context.SaveChangesAsync();
            return phone;
        }

        public int GetPhoneIdByName(string phoneName)
        {
            Phone phone = this._context.Phones.Where(p => p.Name == phoneName).FirstOrDefault();
            if (phone is null)
            {
                return 0;
            }
            return phone.Id;
        }
    }
}

