using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion; // Adicione esta diretiva
using AutoTrackMinimalApi.Domain.DTOs.Enuns;
using AutoTrackMinimalApi.Domain.Entity;


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
            .Property(a => a.Profile)
            .HasConversion(new EnumToStringConverter<Profile>());

            mb.Entity<Administrator>()
            .HasData(
                new Administrator
                {
                    Id = 1,
                    Email = "admin@teste.com",
                    Password = "12345678",
                    Profile = Profile.Admin
                }
           );
        }
        public DbSet<Administrator>? Administrators { get; set; } = default;
        public DbSet<Vehicle>? Vehicles { get; set; } = default;

    }
}