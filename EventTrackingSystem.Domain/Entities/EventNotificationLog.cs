namespace EventTrackingSystem.Domain.Entities;

public class EventNotificationLog
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int UserId { get; set; }
    public long ChatId { get; set; }
    public DateTime SentAt { get; set; }
}
