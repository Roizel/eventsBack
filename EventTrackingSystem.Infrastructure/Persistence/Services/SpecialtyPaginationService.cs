using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class SpecialtyPaginationService(
AppDbContext context,
IMapper mapper
) : PaginationService<SpecialtyEntity, SpecialtyDto, SpecialtyFilterDto>(mapper)
{
    protected override IQueryable<SpecialtyEntity> GetQuery() => context.Specialties;

    protected override IQueryable<SpecialtyEntity> FilterQuery(IQueryable<SpecialtyEntity> query, SpecialtyFilterDto paginationVm)
    {
        return query;
    }
}
