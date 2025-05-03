using System.ComponentModel.DataAnnotations;

namespace EventTrackingSystem.Domain.Entities;

public class GalleryPhotoEntity
{
    public int Id { get; set; }

    [Required]
    public string PhotoPath { get; set; } = null!;

    public int GalleryId { get; set; }
    public GalleryEntity Gallery { get; set; } = null!;
}
