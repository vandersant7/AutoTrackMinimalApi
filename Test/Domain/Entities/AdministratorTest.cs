using AutoTrackMinimalApi.Domain.Entity;
using AutoTrackMinimalApi.Domain.DTOs.Enuns;


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
                Profile = Profile.Admin
            };

            // Act & Assert
            Assert.AreEqual(1, admin.Id);
            Assert.AreEqual("admin@teste.com", admin.Email);
            Assert.AreEqual("12345678", admin.Password);
            Assert.AreEqual<AutoTrackMinimalApi.Domain.DTOs.Enuns.Profile>(
                            AutoTrackMinimalApi.Domain.DTOs.Enuns.Profile.Admin,
                                admin.Profile
            );



        }
    }
}