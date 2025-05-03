using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackingSystem.Infrastructure.Persistence.Repositories;

public class SpecialtyRepository(AppDbContext context) : ISpecialtyRepository
{

    public async Task<IEnumerable<SpecialtyEntity>> GetAllAsync()
    {
        return await context.Specialties.ToListAsync();
    }

    public async Task<SpecialtyEntity?> GetByIdAsync(int id)
    {
        return await context.Specialties.FindAsync(id);
    }

    public async Task AddAsync(SpecialtyEntity specialty)
    {
        context.Specialties.Add(specialty);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SpecialtyEntity specialty)
    {
        context.Specialties.Update(specialty);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var specialty = await context.Specialties.FindAsync(id);
        if (specialty != null)
        {
            context.Specialties.Remove(specialty);
            await context.SaveChangesAsync();
        }
    }
}