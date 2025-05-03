using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTrackingSystem.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController(
    IUserService service
) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var user = await service.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var users = await service.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromForm] SignUpDto createVm)
    {
        try
        {
            var token = await service.AddUserAsync(createVm);
            return Ok(new { Token = token });
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await service.DeleteUserAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInDto model)
    {
        try
        {
            var token = await service.SignInAsync(model);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}
