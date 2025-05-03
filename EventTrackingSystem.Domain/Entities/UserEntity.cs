using Microsoft.AspNetCore.Identity;

namespace EventTrackingSystem.Domain.Entities;

public class UserEntity : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Photo { get; set; }
    public long? TelegramChatId { get; set; }

    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = null!;
    public ICollection<EventEntity> Events { get; set; } = null!;

}
