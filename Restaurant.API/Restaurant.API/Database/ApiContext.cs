using Microsoft.EntityFrameworkCore;
using Restaurant.API.Database.Mappers;
using Restaurant.API.Models;
using System.IO;

namespace Restaurant.API.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public ApiContext()
        {
        }

        public DbSet<RestaurantModel> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RestaurantMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}
