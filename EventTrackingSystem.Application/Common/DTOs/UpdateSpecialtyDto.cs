using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class UpdateSpecialtyDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Level { get; set; }
    public string? ShortDescription { get; set; }
    public IFormFile? Photo { get; set; }
}
