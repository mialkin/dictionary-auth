using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromQuery] string username,
        [FromQuery] string password,
        CancellationToken cancellationToken)
    {
        await Task.Yield();

        return Ok();
    }
}
