namespace EventTrackingSystem.Domain.Entities;

public class MediaEntity
{
    public int Id { get; set; }
    public string FilePath { get; set; } = null!;
    public string FileType { get; set; } = null!; // "photo" або "video"
    public int EventId { get; set; }
    public EventEntity Event { get; set; } = null!;
}
