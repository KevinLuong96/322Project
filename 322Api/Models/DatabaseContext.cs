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
        public DbSet<Phone> Phones { get; set; }
        public DbSet<ReviewSource> ReviewSources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "test@test.com",
                    Password = "72BDqJZX4jSa9dkt/Yk8KcA0Ng1YRfFnbJa8cGcTixbWysaR",
                    Role = Roles.Admin,
                });
            modelBuilder.Entity<ReviewSource>().HasData(
                new ReviewSource()
                {
                    Id = 1,
                    SourceName = "TheVerge",
                },
                new ReviewSource()
                {
                    Id = 2,
                    SourceName = "TechRadar",
                },
                new ReviewSource()
                {
                    Id = 3,
                    SourceName = "Cnet",
                });
        }
    }
}

