namespace EventTrackingSystem.Application.Common.DTOs;

public class GalleryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string PreviewPhoto { get; set; } = null!;
    public IEnumerable<GalleryPhotoDto> Photos { get; set; } = new List<GalleryPhotoDto>();
}
