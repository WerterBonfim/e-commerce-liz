using Domain.Entities;
using Domain.Ports;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Query;

public class ClienteQueryHandler : IRequestHandler<BuscarClientePorCpfQuery, Result<Cliente>>
{
    private readonly IClienteRepositorio _clienteRepositorio;
    private ILogger<ClienteQueryHandler> _logger;

    public ClienteQueryHandler(
        IClienteRepositorio clienteRepositorio,
        ILogger<ClienteQueryHandler> logger
    )
    {
        _clienteRepositorio = clienteRepositorio;
        _logger = logger;
    }

    public async Task<Result<Cliente>> Handle(BuscarClientePorCpfQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var validacao = request.Validar();
            if (validacao.IsFailed)
                return validacao;

            var cliente = await _clienteRepositorio.ObterPorCpf(request.Cpf, cancellationToken);

            return cliente;

        }
        catch (Exception e)
        {
            _logger.LogError("Ocorreu um erro ao tentar buscar o cliente por cpf", e);
            throw;
        }
    }
}