using Core;
using FluentValidation;

namespace Domain.Entities.Validators;

public class ValidacaoDeCpf : AbstractValidator<string>
{
    private const int CpfLength = 11;

    private static readonly int[] Multipliers1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    private static readonly int[] Multipliers2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    public ValidacaoDeCpf()
    {
        RuleFor(cpf => cpf)
            .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido("Cpf"))
            .Length(11)
            .WithMessage(MensagemDeErro.CampoDeveTerExatamente("Cpf", 11))
            .Must(SerCpfValido).WithMessage("CPF inválido.");
    }

    private static bool SerCpfValido(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != CpfLength)
        {
            return false;
        }

        cpf = cpf
            .Trim()
            .Replace(".", "")
            .Replace("-", "");

        if (!long.TryParse(cpf, out var numericCpf))
            return false;
        

        var cpfDigits = numericCpf
            .ToString()
            .Select(c => int.Parse(c.ToString()))
            .ToArray();

        var sum = 0;

        for (var i = 0; i < 9; i++)
            sum += cpfDigits[i] * Multipliers1[i];
        

        var remainder = sum % 11;
        var firstVerificationDigit = remainder < 2 ? 0 : 11 - remainder;

        if (cpfDigits[9] != firstVerificationDigit)
            return false;
        
        sum = 0;

        for (var i = 0; i < 10; i++)
            sum += cpfDigits[i] * Multipliers2[i];
        
        remainder = sum % 11;
        var secondVerificationDigit = remainder < 2 ? 0 : 11 - remainder;

        return cpfDigits[10] == secondVerificationDigit;
    }
}