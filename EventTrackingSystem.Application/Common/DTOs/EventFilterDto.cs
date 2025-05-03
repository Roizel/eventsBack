namespace EventTrackingSystem.Application.Common.DTOs;

public class EventFilterDto : PaginationDto
{
    public string? Name { get; set; }
    public bool? IsRandomItems { get; set; }
    public bool? IsCompleted { get; set; }
    public bool? IsPublished { get; set; }
    public bool? OrderToOldest { get; set; }
}
