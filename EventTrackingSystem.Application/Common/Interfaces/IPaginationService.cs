using EventTrackingSystem.Application.Common.DTOs;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IPaginationService<EntityDtoType, PaginationDtoType> where PaginationDtoType : PaginationDto
{
    Task<PageDto<EntityDtoType>> GetPageAsync(PaginationDtoType dto);
}
