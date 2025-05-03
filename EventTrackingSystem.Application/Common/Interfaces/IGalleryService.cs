using EventTrackingSystem.Application.Common.DTOs;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IGalleryService
{
    Task<IEnumerable<GalleryDto>> GetAllAsync();
    Task<GalleryDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateGalleryDto dto);
    Task UpdateAsync(UpdateGalleryDto dto);
    Task DeleteAsync(int id);
}
