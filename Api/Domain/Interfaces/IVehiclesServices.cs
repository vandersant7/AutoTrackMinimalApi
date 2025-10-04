using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrackMinimalApi.Domain.Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoTrackMinimalApi.Domain.Interfaces
{
    public interface IVehiclesServices
    {
        List<Vehicle> GetAllVehicles(int? page = 1, string? name = null, string? model = null);

        Vehicle? GetVehicleById(int id);

        void AddVehicle(Vehicle vehicle);

        void UpdateVehicle(Vehicle vehicle);

        void DeleteVehicle(Vehicle vehicle);
    }
}