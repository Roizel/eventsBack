using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class AchievementPaginationService(
AppDbContext context,
IMapper mapper
) : PaginationService<AchievementEntity, AchievementDto, AchievementFilterDto>(mapper)
{
    protected override IQueryable<AchievementEntity> GetQuery() => context.Achievements;

    protected override IQueryable<AchievementEntity> FilterQuery(IQueryable<AchievementEntity> query, AchievementFilterDto paginationVm)
    {
        return query;
    }
}
