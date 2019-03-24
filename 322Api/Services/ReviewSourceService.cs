using System;
using System.Linq;
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

        public int GetReviewSourceIdBySourceName(string name)
        {
            ReviewSource rs = this._context.ReviewSources.Where(r => r.SourceName == name).FirstOrDefault();
            if (rs is null)
            {
                return 0;
            }
            return rs.Id;
        }
    }
}
