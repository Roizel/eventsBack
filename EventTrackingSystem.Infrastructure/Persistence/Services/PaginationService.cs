using AutoMapper.QueryableExtensions;
using AutoMapper;
using EventTrackingSystem.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using EventTrackingSystem.Application.Common.DTOs;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public abstract class PaginationService<EntityType, EntityDtoType, PaginationDtoType>(
    IMapper mapper
) : IPaginationService<EntityDtoType, PaginationDtoType> where PaginationDtoType : PaginationDto
{
    public async Task<PageDto<EntityDtoType>> GetPageAsync(PaginationDtoType dto)
    {
        if (dto.PageSize is not null && dto.PageIndex is null)
            throw new Exception("PageIndex is required if PageSize is initialized");

        if (dto.PageIndex < 0)
            throw new Exception("PageIndex less than 0");

        if (dto.PageSize < 1)
            throw new Exception("PageSize is invalid");

        var query = GetQuery();

        query = FilterQuery(query, dto);

        int count = await query.CountAsync();

        int pagesAvailable;

        if (dto.PageSize is not null)
        {
            pagesAvailable = (int)Math.Ceiling(count / (double)dto.PageSize);
            query = query
                .Skip((int)dto.PageIndex! * (int)dto.PageSize)
                .Take((int)dto.PageSize);
        }
        else
        {
            pagesAvailable = count > 0 ? 1 : 0;
        }

        var data = await MapAsync(query);

        return new PageDto<EntityDtoType>
        {
            Items = data,
            PagesAvailable = pagesAvailable,
            ItemsAvailable = count
        };
    }

    protected abstract IQueryable<EntityType> GetQuery();

    protected abstract IQueryable<EntityType> FilterQuery(IQueryable<EntityType> query, PaginationDtoType paginationVm);

    protected virtual async Task<IEnumerable<EntityDtoType>> MapAsync(IQueryable<EntityType> query)
    {
        return await query
            .ProjectTo<EntityDtoType>(mapper.ConfigurationProvider)
            .ToArrayAsync();
    }
}