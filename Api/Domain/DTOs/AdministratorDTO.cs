using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrackMinimalApi.Domain.DTOs.Enuns;

namespace AutoTrackMinimalApi.Domain.DTOs
{
    public record AdministratorDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; } = string.Empty;
        public Profile Profile { get; set; }
    }
}