namespace EventTrackingSystem.Application.Common.DTOs;

public class MediaDto
{
    public int Id { get; set; }
    public string FilePath { get; set; } = null!;
    public string FileType { get; set; } = null!;
    public int EventId { get; set; }
}
