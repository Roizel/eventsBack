using System.ComponentModel.DataAnnotations;

namespace EventTrackingSystem.Domain.Entities;

public class GalleryEntity
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    public string PreviewPhotoPath { get; set; } = null!;

    public ICollection<GalleryPhotoEntity> Photos { get; set; } = new List<GalleryPhotoEntity>();
}
