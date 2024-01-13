using MediatR;

namespace Dictionary.Auth.UseCases.Auth.Commands;

public record LoginCommand(string Username, string Password) : IRequest<bool>;
