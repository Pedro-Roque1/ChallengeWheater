using Microsoft.EntityFrameworkCore;
using WheaterAppApi.Models;

namespace WheaterAppApi.Data
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteCity> FavoriteCities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteCity>()
                .HasOne(p => p.User)
                .WithMany(c => c.FavoriteCities)
                .HasForeignKey(p => p.UserId);
        }
    }

}
