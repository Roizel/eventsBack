using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;
public class AddMediaDto
{
    public int EventId { get; set; }
    public string? Description { get; set; } = null!;
    public ICollection<IFormFile> Files { get; set; } = null!;
}
