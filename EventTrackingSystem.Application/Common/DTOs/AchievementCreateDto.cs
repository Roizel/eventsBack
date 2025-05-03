using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class AchievementCreateDto
{
    public string Title { get; set; } = null!;
    public IFormFile Photo { get; set; } = null!;
}
