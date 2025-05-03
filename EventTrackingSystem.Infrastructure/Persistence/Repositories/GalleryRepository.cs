using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackingSystem.Infrastructure.Persistence.Repositories;

public class GalleryRepository(AppDbContext context) : IGalleryRepository
{
    public async Task<IEnumerable<GalleryEntity>> GetAllAsync()
    {
        return await context.Galleries
            .Include(g => g.Photos)
            .ToListAsync();
    }

    public async Task<GalleryEntity?> GetByIdAsync(int id)
    {
        return await context.Galleries
            .Include(g => g.Photos)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task AddAsync(GalleryEntity gallery)
    {
        await context.Galleries.AddAsync(gallery);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GalleryEntity gallery)
    {
        context.Galleries.Update(gallery);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var gallery = await GetByIdAsync(id);
        if (gallery != null)
        {
            context.Galleries.Remove(gallery);
            await context.SaveChangesAsync();
        }
    }
}