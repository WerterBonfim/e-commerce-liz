using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core;
using Core.LogService;
using Domain.Entities;
using Domain.Exceptions;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Commands;

public sealed class UsuarioCommandHandler :
    IRequestHandler<RegistrarUsuarioCommand, Result<string>>,
    IRequestHandler<LoginCommand, Result<string>>

{
    private readonly ILoggerManager _logger;
    private readonly UserManager<UsuarioDaAplicacao> _userManager;
    private readonly SignInManager<UsuarioDaAplicacao> _signInManager;
    private readonly IConfiguration _configuration;

    public UsuarioCommandHandler(
        ILoggerManager logger,
        UserManager<UsuarioDaAplicacao> userManager,
        SignInManager<UsuarioDaAplicacao> signInManager,
        IConfiguration configuration
    )
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }


    public async Task<Result<string>> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = (UsuarioDaAplicacao)request;
        var result = await _userManager.CreateAsync(usuario, request.Senha);

        if (!result.Succeeded)
        {
            var abacaxi = result.Errors
                .Select(x => new FieldError(x.Description));
            return Result.Fail(abacaxi);
        }

        await _signInManager.SignInAsync(usuario, isPersistent: false);

        var token = GenerateJwtToken(usuario);
        return Result.Ok(token);
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(
            request.Email,
            request.Senha,
            isPersistent: false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return Result.Fail("Invalid login attempt.");


        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return Result.Fail("Usuário não existe");

        var token = GenerateJwtToken(user);
        return Result.Ok(token);
    }


    private string GenerateJwtToken(UsuarioDaAplicacao usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtKey = _configuration["JwtKey"]
                     ?? throw new JwtException("Achave Jwt não foi definida no arquivo de configuração");
        var key = Encoding.ASCII.GetBytes(jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Id),
                new Claim(ClaimTypes.Email, usuario.Email),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}