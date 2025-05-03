using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs;

public class AchievementUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public IFormFile? Photo { get; set; }
}
