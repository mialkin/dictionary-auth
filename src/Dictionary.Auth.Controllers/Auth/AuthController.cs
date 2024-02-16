using System.Security.Claims;
using Dictionary.Auth.Controllers.Auth.Constants;
using Dictionary.Auth.Controllers.Auth.Requests;
using Dictionary.Auth.UseCases.Auth.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Auth.Controllers.Auth;

[Authorize]
public class AuthController : ApplicationController
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        // TODO protect against brute-force attack:
        // Consider adding: 1) metrics + alerts 2) captcha

        var result = await Sender.Send(new LoginCommand(request.Username, request.Password), cancellationToken);

        if (!result)
            return BadRequest("Invalid credentials");

        await HttpContext.SignInAsync(
            principal: new ClaimsPrincipal(
                new ClaimsIdentity(
                    claims: new[] { new Claim(type: ClaimTypes.Name, value: request.Username) },
                    authenticationType: DefaultAuthenticationScheme.Name
                )
            ),
            properties: new AuthenticationProperties { IsPersistent = true }
        );

        return Ok(result);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            scheme: DefaultAuthenticationScheme.Name,
            properties: new AuthenticationProperties { IsPersistent = true }
        );

        return Ok();
    }
}
