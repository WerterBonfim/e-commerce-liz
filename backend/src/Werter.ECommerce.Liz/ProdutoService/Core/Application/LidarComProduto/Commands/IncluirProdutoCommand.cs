using Domain.Entities;
using FluentResults;

namespace Application.LidarComProduto.Commands;

public record IncluirProdutoCommand(
    string Nome,
    string Descricao,
    decimal Preco,
    string Categoria
) : IRequest<Result<Produto>>;
