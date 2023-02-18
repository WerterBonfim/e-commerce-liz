using Core;
using Domain.Entities.Validators;
using Domain.Helpers;
using FluentResults;
using FluentValidation;

namespace Domain.Entities;

public sealed class Cliente : EntityBase<Cliente>
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Rg { get; set; }

    public Endereco Endereco { get; set; }


    public override Result Validar()
    {
        var resultado = new Validacao().Validate(this);

        return resultado.IsValid is false
            ? Result.Fail(resultado.Errors.Select(x => x.ErrorMessage))
            : Result.Ok();
    }


    private class Validacao : AbstractValidator<Cliente>
    {
        public Validacao()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Nome)))
                .MaximumLength(100)
                .WithMessage(MensagemDeErro.ExcedeuMaximoDeCaracteres(nameof(Nome), 100));

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Cpf)))
                .Length(11)
                .WithMessage(MensagemDeErro.CampoDeveTerExatamente(nameof(Cpf), 11));
            ;

            RuleFor(x => x.Rg)
                .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Rg)))
                .Length(9)
                .WithMessage(MensagemDeErro.CampoDeveTerExatamente(nameof(Rg), 9));
            ;

            RuleFor(cliente => cliente.Endereco)
                .SetValidator(new ValidacaoDeEndereco());
        }
    }
}