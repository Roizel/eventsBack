using System.ComponentModel.DataAnnotations;

namespace EventTrackingSystem.Domain.Entities;

public class PartnerEntity
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string Website { get; set; } = null!;
    public string Logo { get; set; } = null!;
}