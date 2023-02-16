using FluentResults;
using MediatR;

namespace Application.Commands;

public record LoginCommand(string Email, string Senha) : IRequest<Result<string>>;
