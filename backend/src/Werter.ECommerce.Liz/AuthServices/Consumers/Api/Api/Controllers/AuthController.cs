using System.Net;
using Application.Commands;
using Core;
using Core.LogService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILoggerManager _logger;

    public AuthController(IMediator mediator, ILoggerManager logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpPost("registrar")]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.OK, "applicaton/json")]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResultDto), (int)HttpStatusCode.InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegistrarUsuarioCommand command)
    {
        _logger
            .LogInfo("Realizando novo registro");

        var resultado = await _mediator.Send(command);
        if (resultado.IsSuccess)
            return Ok(new { Token = resultado.Value });

        _logger.LogWarn("Não foi possivel registrar um novo usuario");

        return BadRequest(resultado.ToResultDto());
    }


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        _logger
            .LogInfo("Realizando login");

        var resultado = await _mediator.Send(command);

        if (resultado.IsSuccess)
            return Ok(new { Token = resultado.Value });
        
        _logger.LogWarn("Usuario não conseguiu realizar o login");

        return BadRequest(resultado.ToResultDto());
    }
    
    
    [HttpGet("verificar-token")]
    [Authorize]
    public IActionResult VerificarToken()
    {
        return Ok();
    }
}