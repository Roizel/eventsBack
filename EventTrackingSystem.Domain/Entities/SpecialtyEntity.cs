using System.ComponentModel.DataAnnotations;

namespace EventTrackingSystem.Domain.Entities;

public class SpecialtyEntity
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string Level { get; set; } = null!;

    [MaxLength(100)]
    public string ShortDescription { get; set; } = null!;

    public string Photo { get; set; } = null!;
}