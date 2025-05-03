using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class EventPaginationService(
    AppDbContext context,
    IMapper mapper
) : PaginationService<EventEntity, EventDto, EventFilterDto>(mapper)
{
    protected override IQueryable<EventEntity> GetQuery() => context.Events;

    protected override IQueryable<EventEntity> FilterQuery(IQueryable<EventEntity> query, EventFilterDto paginationVm)
    {
        if (paginationVm.IsRandomItems == true)
        {
            query = query.OrderBy(c => Guid.NewGuid());
        }
        else if (paginationVm.OrderToOldest == true)
        {
            query = query.OrderBy(c => c.Date);
        }
        else
        {
            query = query.OrderByDescending(c => c.Date);
        }

        if (paginationVm.IsPublished == true)
        {
            query = query.Where(c => c.Description != null);
        }
        else
        {
            query = query.Where(c => c.Description == null);
        }


        if (paginationVm.Name is not null)
            query = query.Where(c => c.Title.ToLower().Contains(paginationVm.Name.ToLower()));

        if (paginationVm.IsCompleted is not null)
        {
            var now = DateTime.UtcNow;
            query = paginationVm.IsCompleted.Value
                ? query.Where(c => c.Date.AddHours(0) <= now)
                : query.Where(c => c.Date.AddHours(0) > now);
        }

        return query;
    }
}
