using Core;
using Domain.Helpers;
using FluentResults;

namespace Domain.Entities;

public sealed class Cliente : EntityBase<Cliente>
{
    public string Nome { get; set; }
    public string Cpf { get  ; set; }
    public string Rg { get; set; }

    public Endereco Endereco { get; set; }

    


    public override Result<Cliente> Validar()
    {
        var resultadoValidacao = new ValidacoesBuilder()
            .Campo(Nome).NaoPodeSerNulo().Minimo(2).Maximo(300)
            .Campo(Cpf).NaoPodeSerNulo().Exatamente(11)
            .Campo(Rg).NaoPodeSerNulo().Exatamente(9)
            .Validar();

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        var enderecoInvalido = Endereco.Validar();
        if (enderecoInvalido.IsFailed)
            return Result.Fail(enderecoInvalido.Errors);
        
        
        return Result.Ok(new Cliente
        {
            Id = Id,
            Cpf = Cpf,
            Nome = Nome,
            Rg = Rg,
            Endereco = Endereco
        });
    }


    
}