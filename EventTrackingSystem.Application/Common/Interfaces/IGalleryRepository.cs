using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IGalleryRepository
{
    Task<IEnumerable<GalleryEntity>> GetAllAsync();
    Task<GalleryEntity?> GetByIdAsync(int id);
    Task AddAsync(GalleryEntity gallery);
    Task UpdateAsync(GalleryEntity gallery);
    Task DeleteAsync(int id);
}
