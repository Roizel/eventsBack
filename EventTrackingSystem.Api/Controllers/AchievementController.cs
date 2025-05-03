using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTrackingSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AchievementController(
        IAchievementService service,
        IPaginationService<AchievementDto, AchievementFilterDto> pagination
) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var achievements = await service.GetAllAsync();
            return Ok(achievements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var achievement = await service.GetByIdAsync(id);
            if (achievement == null) return NotFound();
            return Ok(achievement);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] AchievementFilterDto vm)
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
        public async Task<IActionResult> Create([FromForm] AchievementCreateDto dto)
        {
            try
            {
                await service.CreateAsync(dto);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding achievement.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] AchievementUpdateDto dto)
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
}
