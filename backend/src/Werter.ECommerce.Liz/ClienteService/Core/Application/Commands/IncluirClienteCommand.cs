using Domain.Entities;
using Domain.Helpers;
using FluentResults;
using MediatR;

namespace Application.Commands;

public record IncluirClienteCommand(
    string Nome,
    string Cpf,
    string Rg,
    IncluirEnderecoCommand Endereco
) : IRequest<Result>
{
    public static explicit operator Cliente(IncluirClienteCommand command) => new()
    {
        Nome = command.Nome,
        Cpf = command.Cpf.LimparDocumento(),
        Rg = command.Rg.LimparDocumento(),
        Endereco = command.Endereco
    };

}

public record IncluirEnderecoCommand(
    string Logradouro,
    string Numero,
    string Cep,
    string Bairro,
    string? Complemento,
    string Cidade,
    string Estado
)
{
    public static implicit operator Endereco(IncluirEnderecoCommand command) => new Endereco
    {
        Bairro = command.Bairro,
        Cep = command.Cep,
        Cidade = command.Cidade,
        Complemento = command.Complemento,
        Estado = command.Estado,
        Logradouro = command.Logradouro,
        Numero = command.Numero
    };
}