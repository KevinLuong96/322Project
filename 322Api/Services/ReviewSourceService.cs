using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ReviewSource> CreateReviewSource(string sourceName)
        {
            ReviewSource rs;
            rs = new ReviewSource { SourceName = sourceName };
            this._context.ReviewSources.Add(rs);
            await this._context.SaveChangesAsync();
            return rs;
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
