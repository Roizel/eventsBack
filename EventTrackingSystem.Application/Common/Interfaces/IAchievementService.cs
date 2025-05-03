using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Interfaces
{
    public interface IAchievementService
    {
        Task<IEnumerable<AchievementDto>> GetAllAsync();
        Task<AchievementDto?> GetByIdAsync(int id);
        Task CreateAsync(AchievementCreateDto dto);
        Task UpdateAsync(AchievementUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
