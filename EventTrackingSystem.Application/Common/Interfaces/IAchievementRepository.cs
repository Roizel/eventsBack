using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IAchievementRepository
{
    Task<AchievementEntity?> GetByIdAsync(int id);
    Task<IEnumerable<AchievementEntity>> GetAllAsync();
    Task AddAsync(AchievementEntity achievement);
    Task UpdateAsync(AchievementEntity achievement);
    Task DeleteAsync(int id);
}
