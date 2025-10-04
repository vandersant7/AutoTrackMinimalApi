using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrackMinimalApi.Domain.DTOs
{
    public record VehicleDTO
    {
        public string? Name { get; set; }
        public string? Model { get; set; }
        public int YearFabrication { get; set; }
    }
}