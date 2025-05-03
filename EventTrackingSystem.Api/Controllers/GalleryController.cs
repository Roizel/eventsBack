using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTrackingSystem.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class GalleryController(
    IGalleryService service,
    IPaginationService<GalleryDto, GalleryFilterDto> pagination
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var galleries = await service.GetAllAsync();
        return Ok(galleries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var gallery = await service.GetByIdAsync(id);
        if (gallery == null)
            return NotFound($"Gallery with id {id} not found.");

        return Ok(gallery);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] GalleryFilterDto vm)
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
    public async Task<IActionResult> Create([FromForm] CreateGalleryDto dto)
    {
        try
        {
            await service.CreateAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the gallery: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateGalleryDto dto)
    {
        try
        {
            await service.UpdateAsync(dto);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the gallery: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the gallery: {ex.Message}");
        }
    }
}
