using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class CreatePartnerDto
{
    public string Name { get; set; } = null!;
    public string Website { get; set; } = null!;
    public IFormFile Logo { get; set; } = null!;
}
