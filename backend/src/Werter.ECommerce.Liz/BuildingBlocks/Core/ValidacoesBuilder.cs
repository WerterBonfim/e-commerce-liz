using System.Runtime.CompilerServices;
using FluentResults;

namespace Core;

public class ValidacoesBuilder
{
    private readonly List<Result> _results = new();
    private string _conteudoCampo;
    private string _nomeCampo;
    

    public ValidacoesBuilder Campo(string campo,
        [CallerArgumentExpression("campo")] string nomeCampo = "")
    {
        _conteudoCampo = campo;
        _nomeCampo = nomeCampo;

        return this;
    }

    public ValidacoesBuilder Minimo(int qtdMinima)
    {
        var result = Result.FailIf(_conteudoCampo.Length < qtdMinima, MenorQueMinimoDeCaracteres(qtdMinima));
        if (result.IsFailed)
            _results.Add(result);

        return this;
    }

    public ValidacoesBuilder Maximo(int qtdMaxima)
    {
        var result = Result.FailIf(_conteudoCampo.Length > qtdMaxima, ExcedeuMaximoDeCaracteres(qtdMaxima));
        if (result.IsFailed)
            _results.Add(result);

        return this;
    }
    
    public ValidacoesBuilder NaoPodeSerNulo()
    {
        var result = Result.FailIf(string.IsNullOrEmpty(_conteudoCampo), CampoInvalido());
        if (result.IsFailed)
            _results.Add(result);

        return this;
    }
    
    public ValidacoesBuilder Exatamente(int digitos)
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        var result = Result.FailIf(_conteudoCampo?.Length != digitos, CampoQuantidadeInvalida());
        if (result.IsFailed)
            _results.Add(result);

        return this;
    }
    
    // public ValidacoesBuilder JuntarSeHouverErros(Result result)
    // {
    //     if (result.IsFailed)
    //     {
    //         result.Errors.ForEach(x => _results.Add());
    //     }
    // }

    public Result Validar()
    {
        var itens = _results.Cast<ResultBase>().ToArray();
        return Result.Merge(itens);
    }
    
    
    
    private string ExcedeuMaximoDeCaracteres(int qtd)
        => $"O campo {_nomeCampo} não pode ter mais de {qtd} caracteares";

    private string MenorQueMinimoDeCaracteres(int qtd)
        => $"O campo {_nomeCampo} está inválido, tem menos de {qtd} caracteares";

    private string CampoInvalido() => $"Campo {_nomeCampo} está inválido.";
    private string CampoQuantidadeInvalida() => $"Campo {_nomeCampo} tem uma quantidade inválida de caracteares";


    
}