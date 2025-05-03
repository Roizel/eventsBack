using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackingSystem.Infrastructure.Persistence.Repositories;

public class PartnerRepository(AppDbContext context) : IPartnerRepository
{
    public async Task<IEnumerable<PartnerEntity>> GetAllAsync()
    {
        return await context.Partners.ToListAsync();
    }

    public async Task<PartnerEntity?> GetByIdAsync(int id)
    {
        return await context.Partners.FindAsync(id);
    }

    public async Task AddAsync(PartnerEntity partner)
    {
        await context.Partners.AddAsync(partner);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PartnerEntity partner)
    {
        context.Partners.Update(partner);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var partner = await context.Partners.FindAsync(id);
        if (partner != null)
        {
            context.Partners.Remove(partner);
            await context.SaveChangesAsync();
        }
    }
}
