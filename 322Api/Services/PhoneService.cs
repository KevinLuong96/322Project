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
        public async Task<Phone[]> CreatePhones(Phone[] phones)
        {
            List<Phone> createdPhones = new List<Phone> { };
            foreach (Phone p in phones)
            {
                p.Name = p.Name.ToLower().Trim();
                p.LastCrawl = DateTime.Now;
                createdPhones.Add(p);
                this._context.Phones.Add(p);
            }

            await this._context.SaveChangesAsync();
            return createdPhones.ToArray();
        }

        public async Task<Phone> CreatePhone(string phoneName)
        {
            Phone phone;
            phone = new Phone(phoneName.ToLower().Trim(), 0, 0);
            phone.LastCrawl = DateTime.Now;
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
            if (p.Score != 0)
            {
                phone.Score = p.Score;
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
            //List<Phone> similarPhones = new List<Phone> { };
            List<Phone> matchedPhones = new List<Phone> { };
            SortedDictionary<int, List<Phone>> similarPhones =
                new SortedDictionary<int, List<Phone>> { };
            Phone[] allPhones = this._context.Phones.ToArray();

            Fastenshtein.Levenshtein lev;
            foreach (Phone phone in allPhones)
            {
                int minDistance = Math.Max(phoneName.Length, phone.Name.Length);
                string[] words = phone.Name.Split(" ");
                foreach (string q in phoneName.Split(" "))
                {
                    lev = new Fastenshtein.Levenshtein(q);
                    foreach (string word in words)
                    {
                        int tempDistance = lev.DistanceFrom(word);
                        if (tempDistance == Math.Max(q.Length, word.Length))
                        {
                            continue;
                        }
                        minDistance = Math.Min(minDistance, tempDistance);
                    }
                }


                //Arbitrary number chosen for similarity necessary
                //create distance to list of phone mapping
                if (minDistance < 3)
                {
                    matchedPhones.Add(phone);
                }
            }

            foreach (Phone phone in matchedPhones)
            {
                lev = new Fastenshtein.Levenshtein(phoneName);
                int distance = lev.DistanceFrom(phone.Name);

                if (similarPhones.ContainsKey(distance))
                {
                    similarPhones[distance].Add(phone);
                }
                else
                {
                    similarPhones.Add(distance, new List<Phone> { phone });
                }
            }

            List<Phone> sortedPhones = new List<Phone> { };
            foreach (List<Phone> phones in similarPhones.Values)
            {
                sortedPhones.AddRange(phones);
            }

            return sortedPhones.ToArray();
        }
    }
}

