using System.Security.Claims;
using Dictionary.Auth.Controllers.Auth.Constants;
using Dictionary.Auth.Controllers.Auth.Requests;
using Dictionary.Auth.UseCases.Auth.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Auth.Controllers.Auth;

public class AuthController : ApplicationController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
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
}
