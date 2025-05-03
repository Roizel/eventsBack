using Microsoft.AspNetCore.Identity;

namespace EventTrackingSystem.Domain.Entities;

public class RoleEntity : IdentityRole<int>
{
    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = null!;
    public virtual ICollection<RoleEventEntity> RoleEvents { get; set; } = null!;

}
