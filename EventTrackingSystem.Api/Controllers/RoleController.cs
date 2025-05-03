using EventTrackingSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventTrackingSystem.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController(
    AppDbContext context
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await context.Roles
            .Where(r => r.Name != "Адміністратор")
            .ToListAsync();

        return Ok(roles);
    }
}