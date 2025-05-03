namespace EventTrackingSystem.Domain.Entities;

public class EventEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public int OrganizerId { get; set; }
    public UserEntity Organizer { get; set; } = null!;
    public string Preview { get; set; } = null!;
    public string? PreviewPhoto { get; set; }

    public virtual ICollection<RoleEventEntity> RoleEvents { get; set; } = new List<RoleEventEntity>();
    public virtual ICollection<MediaEntity> Media { get; set; } = new List<MediaEntity>();
}
