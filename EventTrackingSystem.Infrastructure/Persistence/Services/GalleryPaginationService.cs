using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class GalleryPaginationService(
AppDbContext context,
IMapper mapper
) : PaginationService<GalleryEntity, GalleryDto, GalleryFilterDto>(mapper)
{
    protected override IQueryable<GalleryEntity> GetQuery() => context.Galleries;

    protected override IQueryable<GalleryEntity> FilterQuery(IQueryable<GalleryEntity> query, GalleryFilterDto paginationVm)
    {
        return query;
    }
}
