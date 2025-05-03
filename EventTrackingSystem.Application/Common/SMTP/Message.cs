namespace EventTrackingSystem.Application.Common.SMTP;

public class Message
{
    public string To { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Preview { get; set; } = null!;
    public string PreviewImg { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime Date { get; set; }
}
