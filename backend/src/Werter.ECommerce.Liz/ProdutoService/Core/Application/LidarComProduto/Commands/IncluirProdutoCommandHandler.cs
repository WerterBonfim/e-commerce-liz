using Core.LogService;
using Domain.Entities;
using Domain.Ports;
using FluentResults;

namespace Application.LidarComProduto.Commands;

public class IncluirProdutoCommandHandler : IRequestHandler<IncluirProdutoCommand, Result>
{
    private readonly ILoggerManager _logger;
    private readonly IProdutoRepository _repositorio;

    public IncluirProdutoCommandHandler(
        ILoggerManager logger,
        IProdutoRepository repositorio)
    {
        _logger = logger;
        _repositorio = repositorio;
    }

    public async Task<Result> Handle(IncluirProdutoCommand request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInfo("Tentativa de inserir produto foi cancelada via cancellationToken");
            return Result.Fail("Requisição foi cancelada pelo cancellationToken");
        }

        var produto = new Produto
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            Preco = request.Preco,
            Categorias = request.Categoria
        };

        var resultado = produto.Validar();

        if (resultado.IsFailed)
        {
            _logger.LogInfo("Não inseriru o produto. Não passou na validação");
            return resultado.ToResult();
        }

        var resultadoInsersao = await _repositorio.InserirAsync(resultado.Value, cancellationToken);

        if (resultado.IsSuccess)
            _logger.LogInfo($"Produto inserido com sucesso. ProdutoID: {{resultado.Value.Id}}");

        return resultadoInsersao;
    }
}