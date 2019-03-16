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

        //public Review[] QueryReviewsByName(string name)
        //{
        //    if (phone is null)
        //    {
        //        return null;
        //    }

        //    return QueryReviewsById(phone.Id);
        //}

        public Review[] QueryReviewsById(int Id)
        {
            Review[] reviews;
            reviews = this._context.Reviews.Where(r => r.PhoneId == Id).ToArray();

            return reviews;
        }


    }
}
