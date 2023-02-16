using System.Net;
using Core;
using Domain.Helpers;
using FluentResults;

namespace Domain.Entities;

public class ValidacoeDeCliente
{
    public static Result CpfValido(string? cpf) =>
        Result.Merge(
            FailIf(string.IsNullOrEmpty(cpf), MensagemDeErro.CampoInvalido("Cpf")),
            FailIf(cpf?.LimparDocumento().Length != 11, "Campo CPF tem uma quantidade inválida")
        );

    private static Result FailIf(
        bool isFailure,
        string message,
        HttpStatusCode code = HttpStatusCode.BadRequest
    ) => Result.FailIf(isFailure, new FieldError(message, code));
}