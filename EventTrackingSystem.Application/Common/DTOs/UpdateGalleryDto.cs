using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class UpdateGalleryDto
{
    public int Id { get; set; }
    public string? Title { get; set; } = null!;
    public IFormFile? PreviewPhoto { get; set; }
    public IEnumerable<IFormFile> Photos { get; set; } = new List<IFormFile>();
    public ICollection<int> PhotosToDelete { get; set; } = new List<int>();
}

