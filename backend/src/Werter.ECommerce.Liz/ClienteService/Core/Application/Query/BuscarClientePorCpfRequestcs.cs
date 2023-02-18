using Domain.Entities;
using Domain.Entities.Validators;
using FluentResults;
using MediatR;

namespace Application.Query;

public record BuscarClientePorCpfQuery(string Cpf) : IRequest<Result<Cliente>>
{
    public Result Validar()
    {
        var validacao = new ValidacaoDeCpf().Validate(Cpf);
        
        return validacao.IsValid 
            ? Result.Ok() 
            : Result.Fail(validacao.Errors.First().ErrorMessage);
    }
}
