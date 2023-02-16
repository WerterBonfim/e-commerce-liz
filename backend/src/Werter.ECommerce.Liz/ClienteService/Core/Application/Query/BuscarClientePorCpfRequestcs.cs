using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Query;

public record BuscarClientePorCpfQuery(string Cpf) : IRequest<Result<Cliente>>
{
    public Result<Cliente> Validar() => ValidacoeDeCliente.CpfValido(Cpf);
}
