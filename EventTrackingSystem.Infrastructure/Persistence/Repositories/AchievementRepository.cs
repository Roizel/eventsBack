using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackingSystem.Infrastructure.Persistence.Repositories;

public class AchievementRepository(AppDbContext context) : IAchievementRepository
{
    public async Task<AchievementEntity?> GetByIdAsync(int id)
    {
        return await context.Achievements.FindAsync(id);
    }

    public async Task<IEnumerable<AchievementEntity>> GetAllAsync()
    {
        return await context.Achievements.ToListAsync();
    }

    public async Task AddAsync(AchievementEntity achievement)
    {
        await context.Achievements.AddAsync(achievement);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AchievementEntity achievement)
    {
        context.Achievements.Update(achievement);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var achievement = await GetByIdAsync(id);
        if (achievement != null)
        {
            context.Achievements.Remove(achievement);
            await context.SaveChangesAsync();
        }
    }
}
