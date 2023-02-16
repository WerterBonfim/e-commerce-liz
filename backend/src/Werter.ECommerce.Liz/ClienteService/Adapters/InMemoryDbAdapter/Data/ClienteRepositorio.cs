using Domain.Entities;
using Domain.Entities.Comparers;
using Domain.Ports;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Data;

public sealed class ClienteRepositorio : IClienteRepositorio
{
    private HashSet<Cliente> _clientes = new();

    private readonly ILogger<ClienteRepositorio> _logger;
    
    


    public ClienteRepositorio(ILogger<ClienteRepositorio> logger)
    {
        _logger = logger;
    }

    public ValueTask<Result<Cliente>> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cliente = _clientes.FirstOrDefault(x => x.Id == id);

        return ValueTask.FromResult(cliente is null
            ? Result.Fail("Cliente não encontrado")
            : Result.Ok(cliente));
    }

    public ValueTask<Result<Cliente>> ObterPorCpf(string cpf, CancellationToken cancellationToken)
    {
        var cliente = _clientes.FirstOrDefault(x => x.Cpf == cpf);

        return ValueTask.FromResult(cliente is null
            ? Result.Fail("Cliente não encontrado")
            : Result.Ok(cliente));
    }

    public ValueTask<Result> InserirAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        var jaExiste = _clientes.Contains(cliente, new ClienteIdentityComparer());

        if (jaExiste is false)
            _clientes.Add(cliente);
        
        return ValueTask.FromResult(
            Result.FailIf(jaExiste, "Esse cliente já cadastrado")
        );
    }
}