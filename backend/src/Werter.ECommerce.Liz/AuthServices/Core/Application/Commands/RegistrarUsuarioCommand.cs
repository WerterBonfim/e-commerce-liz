using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Commands;

public record RegistrarUsuarioCommand(
   [Required] string Nome,
   [Required] string SobreNome,
   [Required] string Senha,
   [Required] string Email
) : IRequest<Result<string>>
{

   public static explicit operator UsuarioDaAplicacao(RegistrarUsuarioCommand command) => new()
   {
      UserName = command.Email,
      Nome = command.Nome,
      SobreNome = command.SobreNome,
      Email = command.Email
   };
}