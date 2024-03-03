using MediatR;
using Microsoft.Extensions.Options;

namespace Dictionary.Auth.UseCases.Auth.Commands;

internal class LoginCommandHandler(IOptions<AdminSettings> adminOptions) : IRequestHandler<LoginCommand, bool>
{
    private AdminSettings AdminSettings { get; } = adminOptions.Value;

    public Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = request.Login == AdminSettings.Email
                     && request.Password == AdminSettings.Password;

        return Task.FromResult(result);
    }
}
