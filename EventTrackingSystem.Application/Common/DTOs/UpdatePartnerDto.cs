using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class UpdatePartnerDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Website { get; set; }
    public IFormFile? Logo { get; set; }
}
