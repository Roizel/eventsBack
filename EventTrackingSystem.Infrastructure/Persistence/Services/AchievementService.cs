using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class AchievementService(
    IAchievementRepository repository,
    IImageService imageService,
    IMapper mapper
) : IAchievementService
{
    public async Task<IEnumerable<AchievementDto>> GetAllAsync()
    {
        var achievements = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<AchievementDto>>(achievements);
    }

    public async Task<AchievementDto?> GetByIdAsync(int id)
    {
        var achievement = await repository.GetByIdAsync(id);
        return achievement is null ? null : mapper.Map<AchievementDto>(achievement);
    }

    public async Task CreateAsync(AchievementCreateDto dto)
    {
        var photoPath = await imageService.SaveImageAsync(dto.Photo);
        var achievement = new AchievementEntity
        {
            Title = dto.Title,
            PhotoPath = photoPath
        };

        await repository.AddAsync(achievement);
    }

    public async Task UpdateAsync(AchievementUpdateDto dto)
    {
        var achievement = await repository.GetByIdAsync(dto.Id);
        if (achievement == null)
            throw new KeyNotFoundException($"Achievement with id {dto.Id} not found.");

        if (!string.IsNullOrWhiteSpace(dto.Title))
            achievement.Title = dto.Title;

        if (dto.Photo != null)
        {
            if (!string.IsNullOrWhiteSpace(achievement.PhotoPath))
            {
                imageService.DeleteImage(achievement.PhotoPath);
            }
            achievement.PhotoPath = await imageService.SaveImageAsync(dto.Photo);
        }

        await repository.UpdateAsync(achievement);
    }

    public async Task DeleteAsync(int id)
    {
        var achievement = await repository.GetByIdAsync(id);
        if (achievement != null)
        {
            if (!string.IsNullOrWhiteSpace(achievement.PhotoPath))
            {
                imageService.DeleteImage(achievement.PhotoPath);
            }

            await repository.DeleteAsync(id);
        }
    }
}
