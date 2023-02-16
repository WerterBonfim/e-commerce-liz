using Core;
using FluentResults;

namespace Domain.Entities;

public sealed class Endereco : EntityBase<Endereco>
{
    public string Logradouro { get; set; }

    public string Numero { get; set; }
    public string Cep { get; set; }
    public string Bairro { get; set; }
    public string? Complemento { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    

   


    public override Result<Endereco> Validar()
    {
        var resultadoValidacao = new ValidacoesBuilder()
            .Campo(Logradouro).NaoPodeSerNulo().Minimo(2).Maximo(300)
            .Campo(Numero).NaoPodeSerNulo().Maximo(20)
            .Campo(Cep).NaoPodeSerNulo().Exatamente(8)
            .Campo(Bairro).NaoPodeSerNulo().Minimo(2).Maximo(100)
            .Campo(Cidade).NaoPodeSerNulo().Minimo(2).Maximo(100)
            .Campo(Estado).NaoPodeSerNulo().Exatamente(2)
            .Validar();

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;
        
        return Result.Ok(new Endereco
        {
            Logradouro = Logradouro,
            Bairro = Bairro,
            Cep = Cep,
            Cidade = Cidade,
            Complemento = Complemento,
            Estado = Estado,
            Numero = Numero
        });
    }


 
}
