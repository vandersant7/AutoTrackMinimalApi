using AutoTrackMinimalApi.Domain.DTOs;
using AutoTrackMinimalApi.Domain.Entity;
using AutoTrackMinimalApi.Infrastructure.Database;
using AutoTrackMinimalApi.Infrastructure.Interfaces;

namespace AutoTrackMinimalApi.Domain.Services
{
    public class AdministratorServices : IAdministratorServices
    {

        private readonly AppDbContext _context;

        public AdministratorServices(AppDbContext context)
        {
            _context = context;
        }

        public Administrator GetAdminById(int id)
        {
            return _context.Administrators.Where(v => v.Id == id).FirstOrDefault();
        }

        public List<Administrator> GetAllAdmins(int? page)
        {
            var query = _context.Administrators.AsQueryable();

            int itensPerPage = 10;

            if (page != null)
            {
                query = query.Skip(((int)page - 1) * itensPerPage).Take(itensPerPage);
            }

            return query.ToList();
        }

        public Administrator? Login(LoginDto loginDto)
        {
            var admin = _context.Administrators.Where(a => a.Email == loginDto.Email && a.Password == loginDto.Senha).FirstOrDefault();
            return admin;
        }

        public Administrator? Register(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
            return administrator;
        }

    }
}