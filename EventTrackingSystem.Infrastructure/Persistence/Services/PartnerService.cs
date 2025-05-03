using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class PartnerService(
     IPartnerRepository repository,
     IImageService imageService,
     IMapper mapper
) : IPartnerService
{
    public async Task<IEnumerable<PartnerDto>> GetAllAsync()
    {
        var partners = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<PartnerDto>>(partners);
    }

    public async Task<PartnerDto?> GetByIdAsync(int id)
    {
        var partner = await repository.GetByIdAsync(id);
        return partner is null ? null : mapper.Map<PartnerDto>(partner);
    }

    public async Task CreateAsync(CreatePartnerDto dto)
    {
        var partner = new PartnerEntity
        {
            Name = dto.Name,
            Website = dto.Website,
            Logo = await imageService.SaveImageAsync(dto.Logo)
        };

        await repository.AddAsync(partner);
    }

    public async Task UpdateAsync(UpdatePartnerDto dto)
    {
        var partner = await repository.GetByIdAsync(dto.Id);
        if (partner == null) throw new KeyNotFoundException($"Partner with id {dto.Id} not found.");

        if (!string.IsNullOrWhiteSpace(dto.Name))
            partner.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Website))
            partner.Website = dto.Website;

        if (dto.Logo != null)
        {
            if (!string.IsNullOrWhiteSpace(partner.Logo))
            {
                imageService.DeleteImage(partner.Logo);
            }
            partner.Logo = await imageService.SaveImageAsync(dto.Logo);
        }

        await repository.UpdateAsync(partner);
    }

    public async Task DeleteAsync(int id)
    {
        var partner = await repository.GetByIdAsync(id);
        if (partner != null)
        {
            if (!string.IsNullOrWhiteSpace(partner.Logo))
            {
                imageService.DeleteImage(partner.Logo);
            }

            await repository.DeleteAsync(id);
        }
    }
}
