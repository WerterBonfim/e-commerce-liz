using Core;
using FluentValidation;

namespace Domain.Entities.Validators;

public class ValidacaoDeEndereco : AbstractValidator<Endereco>
{
    public ValidacaoDeEndereco()
    {
        RuleFor(x => x.Logradouro)
            .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Endereco.Logradouro)))
            .MaximumLength(300)
            .WithMessage(MensagemDeErro.ExcedeuMaximoDeCaracteres(nameof(Endereco.Logradouro), 300))
            .MinimumLength(2)
            .WithMessage(MensagemDeErro.MenorQueMinimoDeCaracteres(nameof(Endereco.Logradouro), 2))
            ;

        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Endereco.Numero)))
            .MaximumLength(20)
            .WithMessage(MensagemDeErro.ExcedeuMaximoDeCaracteres(nameof(Endereco.Numero), 20));

        RuleFor(x => x.Cep)
            .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Endereco.Cep)))
            .Length(8)
            .WithMessage(MensagemDeErro.CampoDeveTerExatamente(nameof(Endereco.Cep), 8));

        RuleFor(x => x.Bairro)
            .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Endereco.Bairro)))
            .MaximumLength(100)
            .WithMessage(MensagemDeErro.ExcedeuMaximoDeCaracteres(nameof(Endereco.Bairro), 100))
            .MinimumLength(2)
            .WithMessage(MensagemDeErro.MenorQueMinimoDeCaracteres(nameof(Endereco.Bairro), 2));

        RuleFor(x => x.Cidade)
            .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Endereco.Cidade)))
            .MaximumLength(100)
            .WithMessage(MensagemDeErro.ExcedeuMaximoDeCaracteres(nameof(Endereco.Cidade), 100))
            .MinimumLength(2)
            .WithMessage(MensagemDeErro.MenorQueMinimoDeCaracteres(nameof(Endereco.Cidade), 2));


        RuleFor(x => x.Estado)
            .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Endereco.Estado)))
            .Length(2)
            .WithMessage(MensagemDeErro.CampoDeveTerExatamente(nameof(Endereco.Estado), 2));
    }
}