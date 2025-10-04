using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrackMinimalApi.Domain.DTOs.Enuns;

namespace AutoTrackMinimalApi.Domain.DTOs
{
    public record AdministratorCreateDTO
    {
        public string Email { get; set; } = default;

        public string Password { get; set; } = default;
        
        public Profile? Profile { get; set; } = default;
    }
}