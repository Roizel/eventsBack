using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class GalleryService(
    IGalleryRepository repository,
    IImageService imageService,
    IMapper mapper
) : IGalleryService
{
    public async Task<IEnumerable<GalleryDto>> GetAllAsync()
    {
        var galleries = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<GalleryDto>>(galleries);
    }

    public async Task<GalleryDto?> GetByIdAsync(int id)
    {
        var gallery = await repository.GetByIdAsync(id);
        return gallery == null ? null : mapper.Map<GalleryDto>(gallery);
    }

    public async Task CreateAsync(CreateGalleryDto dto)
    {
        var gallery = new GalleryEntity
        {
            Title = dto.Title,
            PreviewPhotoPath = await imageService.SaveImageAsync(dto.PreviewPhoto),
            Photos = new List<GalleryPhotoEntity>()
        };

        foreach (var photo in dto.Photos)
        {
            gallery.Photos.Add(new GalleryPhotoEntity
            {
                PhotoPath = await imageService.SaveImageAsync(photo)
            });
        }

        await repository.AddAsync(gallery);
    }

    public async Task UpdateAsync(UpdateGalleryDto dto)
    {
        var gallery = await repository.GetByIdAsync(dto.Id);
        if (gallery == null) throw new KeyNotFoundException($"Gallery with id {dto.Id} not found.");


        if (dto.Title != null)
        {
            gallery.Title = dto.Title;
        }
        if (dto.PreviewPhoto != null)
        {
            imageService.DeleteImage(gallery.PreviewPhotoPath);
            gallery.PreviewPhotoPath = await imageService.SaveImageAsync(dto.PreviewPhoto);
        }

        foreach (var photoId in dto.PhotosToDelete)
        {
            var photoToDelete = gallery.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photoToDelete != null)
            {
                imageService.DeleteImage(photoToDelete.PhotoPath);
                gallery.Photos.Remove(photoToDelete);
            }
        }

        foreach (var photo in dto.Photos)
        {
            gallery.Photos.Add(new GalleryPhotoEntity
            {
                PhotoPath = await imageService.SaveImageAsync(photo)
            });
        }

        await repository.UpdateAsync(gallery);
    }

    public async Task DeleteAsync(int id)
    {
        var gallery = await repository.GetByIdAsync(id);
        if (gallery != null)
        {
            imageService.DeleteImage(gallery.PreviewPhotoPath);

            foreach (var photo in gallery.Photos)
            {
                imageService.DeleteImage(photo.PhotoPath);
            }

            await repository.DeleteAsync(id);
        }
    }
}
