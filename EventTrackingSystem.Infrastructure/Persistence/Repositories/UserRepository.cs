using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackingSystem.Infrastructure.Persistence.Repositories;

public class UserRepository(
 AppDbContext context
) : IUserRepository
{
    public async Task AddAsync(UserEntity user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await context.Users
           .Include(c => c.UserRoles)
           .ToListAsync();
    }

    public async Task<UserEntity> GetByIdAsync(int id)
    {
        var user = await context.Users
            .Include(c => c.UserRoles)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user ?? throw new KeyNotFoundException("User not found");
    }


    public async Task UpdateAsync(UserEntity user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}
