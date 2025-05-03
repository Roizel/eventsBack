using EventTrackingSystem.Application.Common.DTOs;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IPartnerService
{
    Task<IEnumerable<PartnerDto>> GetAllAsync();
    Task<PartnerDto?> GetByIdAsync(int id);
    Task CreateAsync(CreatePartnerDto dto);
    Task UpdateAsync(UpdatePartnerDto dto);
    Task DeleteAsync(int id);
}
