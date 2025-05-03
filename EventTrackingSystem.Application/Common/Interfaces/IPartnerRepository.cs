using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IPartnerRepository
{
    Task<IEnumerable<PartnerEntity>> GetAllAsync();
    Task<PartnerEntity?> GetByIdAsync(int id);
    Task AddAsync(PartnerEntity partner);
    Task UpdateAsync(PartnerEntity partner);
    Task DeleteAsync(int id);
}
