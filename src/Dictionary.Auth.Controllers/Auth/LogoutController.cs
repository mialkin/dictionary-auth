using Dictionary.Auth.Controllers.Auth.Constants;
using Dictionary.Auth.Controllers.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dictionary.Auth.Controllers.Auth;

[Authorize]
public class LogoutController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Index([FromServices] IOptions<LogoutSettings> options)
    {
        await HttpContext.SignOutAsync(
            scheme: DefaultAuthenticationScheme.Name,
            properties: new AuthenticationProperties { IsPersistent = true }
        );

        return Redirect(options.Value.RedirectUri!);
    }
}
