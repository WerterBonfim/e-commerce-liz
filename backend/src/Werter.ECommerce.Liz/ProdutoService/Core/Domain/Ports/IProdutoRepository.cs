using Domain.Entities;
using FluentResults;

namespace Domain.Ports;

public interface IProdutoRepository
{
    Task<Produto> Obter(Guid id);
    Task<Result> InserirAsync(Produto produto, CancellationToken cancellationToken);
}