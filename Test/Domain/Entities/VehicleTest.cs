using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrackMinimalApi.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Domain.Entities
{
    [TestClass]
    public class VehicleTest
    {
        [TestMethod]
        public void TestGetVehicle()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Id = 1,
                Name = "Ford",
                Model = "Fiesta",
                YearFabrication = 2020
            };

            // Act & Assert
            Assert.AreEqual(1, vehicle.Id);
            Assert.AreEqual("Ford", vehicle.Name);
            Assert.AreEqual("Fiesta", vehicle.Model);
            Assert.AreEqual(2020, vehicle.YearFabrication);
        }
    }
}