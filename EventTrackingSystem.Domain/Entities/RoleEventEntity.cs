namespace EventTrackingSystem.Domain.Entities;

public class RoleEventEntity
{
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    public int EventId { get; set; }
    public EventEntity Event { get; set; } = null!;
}
