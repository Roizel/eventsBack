using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class SignUpDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public IFormFile? Image { get; set; } = null!;
    public string? RoleId { get; set; }
}
