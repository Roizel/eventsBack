using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackingSystem.Infrastructure.Persistence.Repositories;

public class EventRepository(
 AppDbContext context
) : IEventRepository
{
    public async Task<EventEntity> GetByIdAsync(int id)
    {
        var ev = await context.Events
            .Include(e => e.Organizer)
            .Include(e => e.RoleEvents)
            .ThenInclude(re => re.Role)
            .Include(e => e.Media)
            .FirstOrDefaultAsync(e => e.Id == id);

        return ev ?? throw new KeyNotFoundException("Event not found");
    }

    public async Task<IEnumerable<EventEntity>> GetAllAsync()
    {
        return await context.Events
            .Include(e => e.Organizer)
            .Include(e => e.RoleEvents)
            .ThenInclude(re => re.Role)
            .Include(e => e.Media)
            .ToListAsync();
    }

    public async Task AddAsync(EventEntity eventEntity)
    {
        await context.Events.AddAsync(eventEntity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventEntity eventEntity)
    {
        context.Events.Update(eventEntity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var eventEntity = await GetByIdAsync(id);
        if (eventEntity != null)
        {
            context.Events.Remove(eventEntity);
            await context.SaveChangesAsync();
        }
    }
}
