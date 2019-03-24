using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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


        public Review[] QueryReviewsById(int Id)
        {
            Review[] reviews;
            reviews = this._context.Reviews.Where(r => r.PhoneId == Id).ToArray();

            return reviews;
        }

        public bool IsReviewUnique(int phoneId, int sourceId)
        {
            Review review = this._context.Reviews
                .Where(r => r.PhoneId == phoneId && r.SourceId == sourceId).FirstOrDefault();

            if (!(review is null))
            {
                return false;
            }

            return true;
        }

        public async Task SaveReviews(Review[] reviews)
        {
            var tasks = new List<Task> { };

            foreach (Review review in reviews)
            {
                tasks.Add(this._context.AddAsync(review));
            }
            await Task.WhenAll(tasks);
            _context.SaveChanges();

            return;
        }
    }
}
