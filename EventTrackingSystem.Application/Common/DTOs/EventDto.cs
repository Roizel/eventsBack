namespace EventTrackingSystem.Application.Common.DTOs;
public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public int OrganizerId { get; set; }
    public UserDto Organizer { get; set; } = null!;
    public string Preview { get; set; } = null!;
    public string? PreviewPhoto { get; set; }
    public List<RoleDto> Roles { get; set; } = new();
    public List<MediaDto> Media { get; set; } = new();
}
