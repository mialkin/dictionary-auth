using System.Security.Claims;
using Dictionary.Auth.Controllers.Auth.Constants;
using Dictionary.Auth.Controllers.Auth.Requests;
using Dictionary.Auth.Controllers.Auth.Settings;
using Dictionary.Auth.UseCases.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dictionary.Auth.Controllers.Auth;

[Authorize]
[Route("[controller]")]
public class AuthController(ISender sender) : Controller
{
    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromServices] IOptions<AuthSettings> options,
        [FromForm] LoginRequest request, CancellationToken cancellationToken)
    {
        // TODO protect against brute-force attack:
        // Consider adding: 1) metrics + alerts 2) captcha
        var result = await sender.Send(new LoginCommand(request.Username, request.Password), cancellationToken);

        if (!result)
        {
            ViewBag.ErrorMessage = "Invalid credentials";
            return View();
        }

        await HttpContext.SignInAsync(
            principal: new ClaimsPrincipal(
                new ClaimsIdentity(
                    claims: new[] { new Claim(type: ClaimTypes.Name, value: request.Username) },
                    authenticationType: DefaultAuthenticationScheme.Name
                )
            ),
            properties: new AuthenticationProperties { IsPersistent = true }
        );

        return Redirect(options.Value.RedirectUri!);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromServices] IOptions<AuthSettings> options)
    {
        await HttpContext.SignOutAsync(
            scheme: DefaultAuthenticationScheme.Name,
            properties: new AuthenticationProperties { IsPersistent = true }
        );

        return Redirect(options.Value.RedirectUri!);
    }
}
