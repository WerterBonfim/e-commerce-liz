using Core;
using Domain.Entities.Validators;
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



    public override Result Validar()
    {
        var resultado = new ValidacaoDeEndereco().Validate(this);
        
        return resultado.IsValid is false
            ? Result.Fail(resultado.Errors.Select(x => x.ErrorMessage)) 
            : Result.Ok();
    }
    

 
}
