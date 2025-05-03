using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class CreateEventDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public string? Location { get; set; } = null!;
    public int? OrganizerId { get; set; }
    public string? Preview { get; set; } = null!;
    public IFormFile? PreviewPhoto { get; set; }
    public List<int>? RolesIds { get; set; } = null!;
}
