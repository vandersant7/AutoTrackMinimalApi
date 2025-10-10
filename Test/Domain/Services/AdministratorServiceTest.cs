using AutoTrackMinimalApi.Domain.Entity;
using AutoTrackMinimalApi.Domain.Services;
using AutoTrackMinimalApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using AutoTrackMinimalApi.Domain.DTOs.Enuns;

namespace Test.Domain.Services
{
    [TestClass]
    public class AdministratorServiceTest
    {
        // Updated method to create an InMemory test database context 
        private AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [TestMethod]
        public void TestGetAdministrator()
        {
            // Arrange
            var context = CreateDbContext();
            var service = new AdministratorServices(context);
            var admin = new Administrator
            {
                Id = 1,
                Email = "admin@getadmin.com",
                Password = "password",
                Profile = Profile.Admin
            };
            service.Register(admin);

            // Act
            var result = service.GetAllAdmins(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("admin@getadmin.com", result.First().Email);
        }

        [TestMethod]
        public void TestGetAllAdministrator()
        {
            // Arrange
            var context = CreateDbContext();
            var service = new AdministratorServices(context);
            service.Register(new Administrator
            {
                Id = 1,
                Email = "admin1@test.com",
                Password = "pwd1",
                Profile = Profile.Admin
            });
            service.Register(new Administrator
            {
                Id = 2,
                Email = "admin2@test.com",
                Password = "pwd2",
                Profile = Profile.Admin
            });

            // Act
            var admins = service.GetAllAdmins(0);

            // Assert
            Assert.HasCount(2, admins);
        }

        [TestMethod]
        public void TestSaveAdministrator()
        {
            // Arrange
            var admin = new Administrator
            {
                Id = 1,
                Email = "admin2@teste.com",
                Password = "12345678",
                Profile = Profile.Admin
            };
            var context = CreateDbContext();
            var service = new AdministratorServices(context);

            // Act
            service.Register(admin);

            // Assert
            Assert.HasCount(1, service.GetAllAdmins(0));
        }
        
    }
}