using FluentResults;

namespace Application.ProdutoFeature.Ports;

public interface IProdutoManager
{
    Task<Result> CriarProdutoAsync();
}