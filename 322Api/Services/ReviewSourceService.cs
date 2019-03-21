using System;
using _322Api.Models;
namespace _322Api.Services
{
    public class ReviewSourceService
    {
        private readonly DatabaseContext _context;

        public ReviewSourceService(DatabaseContext context)
        {
            this._context = context;
        }

        public bool CreateReviewSource(string sourceName)
        {
            return true;
        }
    }
}
