using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrackMinimalApi.Domain.ModelViews
{
    public record AdmLoggedInDto
    {
        public string? Email { get; set; } = default;
        public string? Profile { get; set; } = default;
        public string? Token { get; set; } = default;
    }
}