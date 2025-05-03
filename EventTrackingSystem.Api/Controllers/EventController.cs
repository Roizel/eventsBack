using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventTrackingSystem.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class EventController(
    IEventService service,
    IPaginationService<EventDto, EventFilterDto> pagination
) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var eventDto = await service.GetByIdAsync(id);
        if (eventDto == null)
        {
            return NotFound();
        }

        return Ok(eventDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await service.GetAllAsync();

        return Ok(events);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] EventFilterDto vm)
    {
        try
        {
            return Ok(await pagination.GetPageAsync(vm));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateEventDto model)
    {
        var eventId = await service.CreateAsync(model);
        return Ok(new { id = eventId });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateEventDto model)
    {
        try
        {
            await service.UpdateAsync(model);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddMedia([FromForm] AddMediaDto model)
    {
        try
        {
            await service.AddMediaToEventAsync(model);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while adding media to the event.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMedia([FromForm] UpdateMediaDto model)
    {
        try
        {
            await service.UpdateMediaAsync(model);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating media to the event.");
        }
    }
}
