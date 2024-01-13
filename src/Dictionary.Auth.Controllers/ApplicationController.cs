using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApplicationController : ControllerBase {
    private ISender? _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
