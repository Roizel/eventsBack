namespace EventTrackingSystem.Application.Common.DTOs;

public class SpecialtyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string? ShortDescription { get; set; }
    public string? Photo { get; set; }
}
