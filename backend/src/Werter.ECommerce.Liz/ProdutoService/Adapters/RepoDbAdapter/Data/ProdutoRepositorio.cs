using Domain.Entities;
using Domain.Ports;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using RepoDb;

namespace Data;

public class ProdutoRepositorio : BaseRepository<Produto, SqlConnection>, IProdutoRepository
{
    private readonly ILogger<ProdutoRepositorio> _logger;

    public ProdutoRepositorio(
        string connectionString, 
        ILogger<ProdutoRepositorio> logger) : base(connectionString, commandTimeout: 2)
    {
        _logger = logger;

        FluentMapper.Entity<Produto>()
            .Table("TB_Produtos");
    }

    public async Task<Produto?> Obter(Guid id)
    {
        return (await QueryAsync(id))
            .FirstOrDefault();
    }

    public async Task<Result> InserirAsync(Produto produto, CancellationToken cancellationToken)
    {
        try
        {

            var resultaod = await InsertAsync(produto, cancellationToken: cancellationToken);
            return Result.Ok();

        }
        catch (SqlException e)
        {
            Result.Fail("Erro ao tentar inserir um novo produto");
            throw;
        }
    }

    private async Task<R> ExecutarComando<R>(Func<Task<R>> acao, string mensagemErro)
    {
        try
        {
            return await acao();
            
        }
        catch (SqlException sqlException)
        {
            _logger.LogError(sqlException, mensagemErro);
            throw;
        }
    }
}