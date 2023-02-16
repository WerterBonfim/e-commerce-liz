using Domain.Entities;
using FluentResults;

namespace Domain.Ports;

public interface IClienteRepositorio
{
    ValueTask<Result<Cliente>> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    ValueTask<Result<Cliente>> ObterPorCpf(string cpf, CancellationToken cancellationToken);
    ValueTask<Result> InserirAsync(Cliente cliente, CancellationToken cancellationToken);
}