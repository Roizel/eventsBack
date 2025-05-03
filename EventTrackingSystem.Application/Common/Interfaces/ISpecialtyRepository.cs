using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface ISpecialtyRepository
{
    Task<IEnumerable<SpecialtyEntity>> GetAllAsync();
    Task<SpecialtyEntity?> GetByIdAsync(int id);
    Task AddAsync(SpecialtyEntity specialty);
    Task UpdateAsync(SpecialtyEntity specialty);
    Task DeleteAsync(int id);
}
