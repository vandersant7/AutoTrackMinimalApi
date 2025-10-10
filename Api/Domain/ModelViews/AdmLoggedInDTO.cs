using AutoTrackMinimalApi.Domain.DTOs.Enuns;

namespace AutoTrackMinimalApi.Domain.ModelViews
{
    public record AdmLoggedInDto
    {
        public string? Email { get; set; }
        public Profile Profile { get; set; }
        public string? Token { get; set; }
    }
}