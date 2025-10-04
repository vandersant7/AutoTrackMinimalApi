using AutoTrackMinimalApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AutoTrackMinimalApi.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Administrator>()
            .HasData(
                new Administrator
                {
                    Id = 1,
                    Email = "admin@teste.com",
                    Password = "12345678",
                    Profile = "admin"
                }
           );
        }
        public DbSet<Administrator>? Administrators { get; set; } = default;
        public DbSet<Vehicle>? Vehicles { get; set; } = default;

    }
}