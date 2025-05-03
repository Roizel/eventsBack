namespace EventTrackingSystem.Application.Common.DTOs;

public class GalleryPhotoDto
{
    public int Id { get; set; }
    public string PhotoPath { get; set; } = null!;
    public int GalleryId { get; set; }
}
