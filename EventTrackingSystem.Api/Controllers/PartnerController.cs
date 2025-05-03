using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTrackingSystem.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PartnerController(
    IPartnerService service,
    IPaginationService<PartnerDto, PartnerFilterDto> pagination
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var partners = await service.GetAllAsync();
        return Ok(partners);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var partner = await service.GetByIdAsync(id);
        if (partner == null) return NotFound();
        return Ok(partner);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] PartnerFilterDto vm)
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
    public async Task<IActionResult> Create([FromForm] CreatePartnerDto dto)
    {
        try
        {
            await service.CreateAsync(dto);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while adding partner.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdatePartnerDto dto)
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
