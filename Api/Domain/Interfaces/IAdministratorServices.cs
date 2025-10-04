using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrackMinimalApi.Domain.DTOs;
using AutoTrackMinimalApi.Domain.Entity;

namespace AutoTrackMinimalApi.Infrastructure.Interfaces
{
    public interface IAdministratorServices
    {
        Administrator? Login(LoginDto loginDto);

        Administrator? Register(Administrator administrator);

        List<Administrator> GetAllAdmins(int? page);

        Administrator? GetAdminById(int id);
    }
}