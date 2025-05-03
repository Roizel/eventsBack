using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class UpdateEventDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public string? Location { get; set; }
    public int OrganizerId { get; set; }
    public string? Preview { get; set; }
    public IFormFile? PreviewPhoto { get; set; }
    public List<int>? RolesIds { get; set; }
}
