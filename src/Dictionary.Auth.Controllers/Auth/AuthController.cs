using Dictionary.Auth.Controllers.Auth.Requests;
using Dictionary.Auth.UseCases.Auth.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Auth.Controllers.Auth;

public class AuthController : ApplicationController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new LoginCommand(request.Username, request.Password), cancellationToken);
        return Ok(result);
    }
}
