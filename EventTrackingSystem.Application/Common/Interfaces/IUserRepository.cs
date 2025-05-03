using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> GetByIdAsync(int id);
    Task<IEnumerable<UserEntity>> GetAllAsync();
    Task AddAsync(UserEntity user);
    Task UpdateAsync(UserEntity user);
    Task DeleteAsync(int id);
}

