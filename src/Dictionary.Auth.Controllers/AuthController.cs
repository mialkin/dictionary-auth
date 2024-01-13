using Dictionary.Auth.UseCases.Auth.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Auth.Controllers;

public class AuthController : ApplicationController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromQuery] string username,
        [FromQuery] string password,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new LoginCommand(username, password), cancellationToken);
        return Ok(result);
    }
}
