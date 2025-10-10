using AutoTrackMinimalApi.Domain.Entity;
using AutoTrackMinimalApi.Domain.Interfaces;
using AutoTrackMinimalApi.Infrastructure.Database;

namespace AutoTrackMinimalApi.Domain.Services
{
    public class VehiclesServices : IVehiclesServices
    {
        private readonly AppDbContext _context;

        public VehiclesServices(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Vehicles.Add(vehicle); // Removido ?.
            _context.SaveChanges();
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
        }

        public List<Vehicle> GetAllVehicles(int? page = 1, string? name = null, string? model = null)
        {
            if (_context.Vehicles == null)
            {
                throw new InvalidOperationException("Coleção de veículos não inicializada.");
            }
            var vehicles = _context.Vehicles.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                vehicles = vehicles.Where(v => v.Name == name);
            }
            if (!string.IsNullOrEmpty(model))
            {
                vehicles = vehicles.Where(v => v.Model == model);
            }
            int itensPerPage = 10;
            if (page != null)
            {
                vehicles = vehicles.Skip((int)(page - 1) * itensPerPage).Take(itensPerPage);
            }
            return vehicles.ToList();
        }

        public Vehicle? GetVehicleById(int id)
        {
            if (_context.Vehicles == null)
            {
                throw new InvalidOperationException("Coleção de veículos não inicializada.");
            }
            var vehicle = _context.Vehicles.Where(v => v.Id == id).FirstOrDefault();
            if (vehicle == null) return null;

            return vehicle;
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }
    }
}