using MediatR;

namespace Dictionary.Auth.UseCases.Auth.Commands;

public record LoginCommand(string Login, string Password) : IRequest<bool>;
