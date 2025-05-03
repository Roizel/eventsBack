using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class SpecialtyService(
   ISpecialtyRepository repository,
   IMapper mapper,
   IImageService imageService
) : ISpecialtyService
{


    public async Task<IEnumerable<SpecialtyDto>> GetAllAsync()
    {
        var specialties = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<SpecialtyDto>>(specialties);
    }

    public async Task<SpecialtyDto?> GetByIdAsync(int id)
    {
        var specialty = await repository.GetByIdAsync(id);
        return specialty == null ? null : mapper.Map<SpecialtyDto>(specialty);
    }

    public async Task AddAsync(CreateSpecialtyDto dto)
    {
        var specialty = mapper.Map<SpecialtyEntity>(dto);

        if (dto.Photo != null)
        {
            specialty.Photo = await imageService.SaveImageAsync(dto.Photo);
        }

        await repository.AddAsync(specialty);
    }

    public async Task UpdateAsync(UpdateSpecialtyDto dto)
    {
        var specialty = await repository.GetByIdAsync(dto.Id);
        if (specialty == null)
        {
            throw new KeyNotFoundException($"Specialty with id {dto.Id} not found.");
        }

        if (dto.Name != null)
        {
            specialty.Name = dto.Name;
        }

        if (dto.Level != null)
        {
            specialty.Level = dto.Level;
        }

        if (dto.ShortDescription != null)
        {
            specialty.ShortDescription = dto.ShortDescription;
        }

        if (dto.Photo != null)
        {
            if (!string.IsNullOrEmpty(specialty.Photo))
            {
                imageService.DeleteImage(specialty.Photo);
            }

            specialty.Photo = await imageService.SaveImageAsync(dto.Photo);
        }

        await repository.UpdateAsync(specialty);
    }

    public async Task DeleteAsync(int id)
    {
        var specialty = await repository.GetByIdAsync(id);
        if (specialty == null) return;

        if (!string.IsNullOrEmpty(specialty.Photo))
        {
            imageService.DeleteImage(specialty.Photo);
        }

        await repository.DeleteAsync(id);
    }
}
