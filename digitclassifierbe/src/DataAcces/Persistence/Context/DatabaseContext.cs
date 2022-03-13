using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAcces.Persistence.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<PredictionRating> Ratings { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<History>().HasMany(b => b.Ratings)
                .WithOne(p => p.Prediction).OnDelete(DeleteBehavior.Cascade);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

    }
}
