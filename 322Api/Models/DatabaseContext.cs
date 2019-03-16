using Microsoft.EntityFrameworkCore;

namespace _322Api.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<ReviewSource> ReviewSources { get; set; }
    }
}
