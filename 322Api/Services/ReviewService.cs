using System;
using System.Linq;
using System.Threading.Tasks;
using _322Api.Models;

namespace _322Api.Services
{
    public class ReviewService
    {
        private readonly DatabaseContext _context;

        public ReviewService(DatabaseContext context)
        {
            this._context = context;
        }

        public Review[] QueryReviewsByName(string name)
        {
            Phone phone = this._context.Phones.Where(p => p.Name == name).FirstOrDefault();
            if (phone is null)
            {
                return null;
            }

            Review[] reviews;
            reviews = this._context.Reviews.Where(r => r.PhoneId == phone.Id).ToArray();

            return reviews;
        }


    }
}
