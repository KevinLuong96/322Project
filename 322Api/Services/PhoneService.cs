using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public Phone GetPhoneById(int Id)
        {
            return this._context.Phones.Find(Id);
        }

        public async Task<Phone> CreatePhone(string phoneName)
        {
            Phone phone;
            phone = new Phone { Name = phoneName, Score = 0, LastCrawl = DateTime.Now };
            this._context.Phones.Add(phone);
            await this._context.SaveChangesAsync();
            return phone;
        }

        public async Task<Phone> UpdatePhone(int phoneId, PhonePatch p)
        {
            Phone phone = this.GetPhoneById(phoneId);
            if (p.Price != 0)
            {
                phone.Price = p.Price;
            }
            if (p.ImageUrl != "")
            {
                phone.ImageUrl = p.ImageUrl;
            }
            this._context.Phones.Update(phone);
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

        public Phone[] QueryPhonesByName(string phoneName)
        {
            List<Phone> similarPhones = new List<Phone> { };
            Phone[] allPhones = this._context.Phones.ToArray();

            Fastenshtein.Levenshtein lev = new Fastenshtein.Levenshtein(phoneName);
            foreach (Phone phone in allPhones)
            {
                //Arbitrary number chosen for similarity necessary
                if (lev.DistanceFrom(phone.Name) < 5)
                {
                    similarPhones.Add(phone);
                }
            }
            return similarPhones.ToArray();
        }
    }
}

