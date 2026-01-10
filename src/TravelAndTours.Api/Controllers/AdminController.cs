using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TravelAndTours.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "ADMIN")]
[ApiExplorerSettings(GroupName = "admin")]
public sealed class AdminController : ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping() => Ok(new { ok = true, utc = DateTime.UtcNow });
}
