using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Interfaces;
public interface IEventRepository
{
    Task<EventEntity> GetByIdAsync(int id);
    Task<IEnumerable<EventEntity>> GetAllAsync();
    Task AddAsync(EventEntity eventEntity);
    Task UpdateAsync(EventEntity eventEntity);
    Task DeleteAsync(int id);
}