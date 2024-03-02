using MediatR;

namespace Dictionary.Auth.UseCases.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<bool>;
