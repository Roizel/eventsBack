using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTrackingSystem.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SpecialtyController(
    ISpecialtyService service,
    IPaginationService<SpecialtyDto, SpecialtyFilterDto> pagination
    ) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var specialties = await service.GetAllAsync();
        return Ok(specialties);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] SpecialtyFilterDto vm)
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var specialty = await service.GetByIdAsync(id);
        if (specialty == null) return NotFound($"Спеціальність з ID {id} не знайдена.");
        return Ok(specialty);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateSpecialtyDto dto)
    {
        try
        {
            await service.AddAsync(dto);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while adding specialty.");
        }
    }


    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateSpecialtyDto dto)
    {
        try
        {
            await service.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
