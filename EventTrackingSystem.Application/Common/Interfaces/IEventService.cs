using EventTrackingSystem.Application.Common.DTOs;

namespace EventTrackingSystem.Application.Common.Interfaces;
public interface IEventService
{
    Task<EventDto> GetByIdAsync(int id);
    Task<IEnumerable<EventDto>> GetAllAsync();
    Task<int> CreateAsync(CreateEventDto eventEntity);
    Task UpdateAsync(UpdateEventDto eventEntity);
    Task AddMediaToEventAsync(AddMediaDto model);
    Task DeleteAsync(int id);
    Task UpdateMediaAsync(UpdateMediaDto model);

}
