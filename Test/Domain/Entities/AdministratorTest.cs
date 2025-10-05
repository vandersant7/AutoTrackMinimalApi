using AutoTrackMinimalApi.Domain.Entity;

namespace Test.Domain.Entities
{
    [TestClass]
    public class AdministratorTest
    {
        [TestMethod]
        public void TestGetAdministrator()
        {
            // Arrange
            var admin = new Administrator
            {
                Id = 1,
                Email = "admin@teste.com",
                Password = "12345678",
                Profile = "Admin"
            };

            // Act & Assert
            Assert.AreEqual(1, admin.Id);
            Assert.AreEqual("admin@teste.com", admin.Email);
            Assert.AreEqual("12345678", admin.Password);
            Assert.AreEqual("Admin", admin.Profile);
           


        }
    }
}