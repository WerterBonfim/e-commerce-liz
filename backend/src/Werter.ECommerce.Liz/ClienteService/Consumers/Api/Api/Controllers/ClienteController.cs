using System.Net;
using Application.Commands;
using Application.Query;
using Core;
using Core.LogService;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class ClienteController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILoggerManager _logger;

    public ClienteController(IMediator mediator, ILoggerManager logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpPost]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.OK, "applicaton/json")]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListarProdutos(
        [FromBody] IncluirClienteCommand command,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInfo("Tentando incluir um novo cliente");

        var resultado = await _mediator.Send(command, cancellationToken);

        if (resultado.IsFailed)
        {
            return BadRequest(resultado.ToResultDto());
        }

        return Ok("Cliente cadastrado com sucesso");
    }


    [HttpGet("{cpf:required}")]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.OK, "applicaton/json")]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ObterPorCpf(string cpf, CancellationToken cancellationToken)
    {
        _logger.LogInfo("Tentando incluir um novo cliente");

        var query = new BuscarClientePorCpfQuery(cpf?.LimparDocumento());
        var resultado = await _mediator.Send(query, cancellationToken);

        if (resultado.IsFailed)
            return BadRequest(resultado.ToResult().ToResultDto());


        return Ok(resultado.Value);
    }
}