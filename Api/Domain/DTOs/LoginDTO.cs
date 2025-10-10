using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrackMinimalApi.Domain.DTOs
{
    public class LoginDto
    {
        public string? Email { get; set; } = string.Empty;
        public string? Senha { get; set; } = string.Empty;
    }
}