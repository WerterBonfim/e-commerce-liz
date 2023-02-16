using Core.LogService;
using Domain.Entities;
using Domain.Ports;
using FluentResults;
using MediatR;

namespace Application.Commands;

public class ClienteCommandHandler : IRequestHandler<IncluirClienteCommand, Result>
{
    private readonly ILoggerManager _logger;
    private readonly IClienteRepositorio _clienteRepositorio;

    public ClienteCommandHandler(
        ILoggerManager logger,
        IClienteRepositorio clienteRepositorio
    )
    {
        _logger = logger;
        _clienteRepositorio = clienteRepositorio;
    }

    public async Task<Result> Handle(IncluirClienteCommand request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInfo("Tentativa de inserir produto foi cancelada via cancellationToken");
            return Result.Fail("Requisição foi cancelada pelo cancellationToken");
        }

        var clienteExiste = await _clienteRepositorio.ObterPorCpf(request.Cpf, cancellationToken);
        if (clienteExiste.IsSuccess)
            return Result.Fail("Esse cliente já cadastrado");

        var cliente = (Cliente)request;

        var resultado = cliente.Validar();
        if (resultado.IsFailed)
        {
            _logger.LogInfo("Não inseriru o cliente. Não passou na validação");
            return resultado.ToResult();
        }

        var resultadoInsersao = await _clienteRepositorio.InserirAsync(cliente, cancellationToken);
        
        if (resultado.IsSuccess)
            _logger.LogInfo($"Cliente registrado com sucesso. ClienteID: {{resultado.Value.Id}}");

        return resultadoInsersao;
    }
}