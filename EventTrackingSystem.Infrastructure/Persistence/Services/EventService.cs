using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class EventService(
    IEventRepository repository,
    IUserService userService,
    IMapper mapper,
    IImageService imageService
) : IEventService
{
    public async Task<int> CreateAsync(CreateEventDto model)
    {
        var newEvent = mapper.Map<EventEntity>(model);

        if (model.Date is null)
        {
            newEvent.Date = DateTime.Now.AddDays(-1);
        }


        if (model.Location is null)
            newEvent.Location = "Онлайн";

        if (model.Preview is null)
            newEvent.Preview = "Без анонсу";

        if (model.OrganizerId is null)
        {
            var users = await userService.GetAllUsersAsync();
            if (users != null)
                newEvent.OrganizerId = users.FirstOrDefault().Id;
        }

        if (model.PreviewPhoto != null)
        {
            newEvent.PreviewPhoto = await imageService.SaveImageAsync(model.PreviewPhoto);
        }

        if (model.RolesIds != null)
        {
            newEvent.RoleEvents = model.RolesIds
                .Select(roleId => new RoleEventEntity { RoleId = roleId, Event = newEvent })
                .ToList();
        }

        await repository.AddAsync(newEvent);
        return newEvent.Id;
    }

    public async Task UpdateAsync(UpdateEventDto model)
    {
        var existingEvent = await repository.GetByIdAsync(model.Id);

        if (existingEvent == null)
        {
            throw new KeyNotFoundException($"Event with id {model.Id} not found.");
        }

        if (model.Date != null)
        {
            existingEvent.Date = (DateTime)model.Date;
        }

        if (model.Preview != null)
        {
            existingEvent.Preview = model.Preview;
        }

        if (model.OrganizerId != 0)
        {
            existingEvent.OrganizerId = model.OrganizerId;
        }

        if (model.Title != null)
        {
            existingEvent.Title = model.Title;
        }

        if (model.Description != null)
        {
            existingEvent.Description = model.Description;
        }

        if (model.Location != null)
        {
            existingEvent.Location = model.Location;
        }

        if (model.PreviewPhoto != null)
        {
            if (existingEvent.PreviewPhoto != null)
            {
                imageService.DeleteImage(existingEvent.PreviewPhoto);
            }
            existingEvent.PreviewPhoto = await imageService.SaveImageAsync(model.PreviewPhoto);
        }

        if (model.RolesIds != null)
        {
            existingEvent.RoleEvents.Clear();

            existingEvent.RoleEvents = model.RolesIds
               .Select(roleId => new RoleEventEntity { RoleId = roleId, EventId = existingEvent.Id })
               .ToList();
        }

        await repository.UpdateAsync(existingEvent);
    }

    public async Task DeleteAsync(int id)
    {
        var currentEvent = await repository.GetByIdAsync(id);
        if (currentEvent == null) return;

        if (currentEvent.PreviewPhoto != null)
            imageService.DeleteImage(currentEvent.PreviewPhoto);

        await repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        var events = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<EventDto>>(events);
    }

    public async Task<EventDto> GetByIdAsync(int id)
    {
        var eventEntity = await repository.GetByIdAsync(id);
        if (eventEntity == null)
        {
            throw new KeyNotFoundException($"Event with id {id} not found.");
        }
        return mapper.Map<EventDto>(eventEntity);
    }

    public Task UpdateAsync(EventEntity eventEntity)
    {
        throw new NotImplementedException();
    }

    public async Task AddMediaToEventAsync(AddMediaDto model)
    {
        var eventEntity = await repository.GetByIdAsync(model.EventId);
        if (eventEntity == null)
        {
            throw new KeyNotFoundException($"Event with id {model.EventId} not found.");
        }

        if (model.Description != null)
            eventEntity.Description = model.Description;

        foreach (var file in model.Files)
        {
            string filePath;
            string fileType;

            if (imageService.IsImage(file))
            {
                fileType = "photo";
                filePath = await imageService.SaveImageAsync(file);
            }
            else if (imageService.IsVideo(file))
            {
                fileType = "video";
                filePath = await imageService.SaveVideoAsync(file);
            }
            else
            {
                throw new InvalidOperationException("The provided file is not a valid image or video.");
            }

            var mediaEntity = new MediaEntity
            {
                FilePath = filePath,
                FileType = fileType,
                EventId = model.EventId,
                Event = eventEntity
            };

            eventEntity.Media.Add(mediaEntity);
        }

        await repository.UpdateAsync(eventEntity);
    }

    public async Task UpdateMediaAsync(UpdateMediaDto model)
    {
        var eventEntity = await repository.GetByIdAsync(model.EventId);
        if (eventEntity == null)
        {
            throw new KeyNotFoundException($"Event with id {model.EventId} not found.");
        }

        foreach (var mediaId in model.MediaIdsToDelete)
        {
            var mediaToDelete = eventEntity.Media.FirstOrDefault(m => m.Id == mediaId);
            if (mediaToDelete != null)
            {
                if (mediaToDelete.FileType == "photo")
                {
                    imageService.DeleteImageIfExists(mediaToDelete.FilePath);
                }
                else if (mediaToDelete.FileType == "video")
                {
                    imageService.DeleteVideo(mediaToDelete.FilePath);
                }

                eventEntity.Media.Remove(mediaToDelete);
            }
        }

        foreach (var file in model.NewFiles)
        {
            string filePath;
            string fileType;

            if (imageService.IsImage(file))
            {
                fileType = "photo";
                filePath = await imageService.SaveImageAsync(file);
            }
            else if (imageService.IsVideo(file))
            {
                fileType = "video";
                filePath = await imageService.SaveVideoAsync(file);
            }
            else
            {
                throw new InvalidOperationException("The provided file is not a valid image or video.");
            }

            var mediaEntity = new MediaEntity
            {
                FilePath = filePath,
                FileType = fileType,
                EventId = model.EventId,
                Event = eventEntity
            };

            eventEntity.Media.Add(mediaEntity);
        }

        await repository.UpdateAsync(eventEntity);
    }
}

