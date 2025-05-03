namespace EventTrackingSystem.Application.Common.DTOs;

public class PageDto<T>
{
    public IEnumerable<T> Items { get; set; } = null!;
    public int PagesAvailable { get; set; }
    public int ItemsAvailable { get; set; }
}
