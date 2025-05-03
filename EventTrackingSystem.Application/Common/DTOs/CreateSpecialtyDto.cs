using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class CreateSpecialtyDto
{
    public string Name { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public IFormFile Photo { get; set; } = null!;
}
