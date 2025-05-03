using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class PartnerPaginationService(
AppDbContext context,
IMapper mapper
) : PaginationService<PartnerEntity, PartnerDto, PartnerFilterDto>(mapper)
{
    protected override IQueryable<PartnerEntity> GetQuery() => context.Partners;

    protected override IQueryable<PartnerEntity> FilterQuery(IQueryable<PartnerEntity> query, PartnerFilterDto paginationVm)
    {
        return query;
    }
}
