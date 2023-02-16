using Core;

namespace Tests;

public class ValidacaoBuilderTests
{
    private readonly ValidacoesBuilder _validacoesBuilder = new();
    
    [Fact(DisplayName = "Deve validar corretamente")]
    [Trait("BuildingBlocks > TestHelper", "Validacoes")]
    public void DeveriaValidarCorretamente()
    {
        // [ Arrange ]

        var logradouro = "Rua ali";

        // [ Act ]


        var resultado = _validacoesBuilder
                .Campo(logradouro)
                .NaoPodeSerNulo()
                .Minimo(2)
                .Maximo(300)
                .Validar();
        
        // [ Assert ]

        resultado.IsFailed
            .Should()
            .BeFalse();
    }

    [Fact(DisplayName = "Deve notificar dois erros, campo em branco")]
    [Trait("BuildingBlocks > TestHelper", "Validacoes")]
    public void DeveriaNotificarDoisErrosCampoEmBranco()
    {
        // [ Arrange ]
        
        var logradouro = "";

        // [ Act ]


        var resultado = _validacoesBuilder
            .Campo(logradouro)
            .NaoPodeSerNulo()
            .Minimo(2)
            .Maximo(300)
            .Validar();
        
        // [ Assert ]

        resultado.IsFailed
            .Should()
            .BeTrue();

        var erros = resultado.Errors
            .Select(x => x.Message);

        erros
            .Should()
            .Contain(new[]
            {
                $"Campo {nameof(logradouro)} está inválido.",
                $"O campo {nameof(logradouro)} está inválido, tem menos de {2} caracteares"
            });

    }
    
    [Fact(DisplayName = "Deve notificar erro, campo com mais de 300 caracteares")]
    [Trait("BuildingBlocks > TestHelper", "Validacoes")]
    public void DeveriaNotificarErroCampoComMaisDe300Caracteres()
    {
        // [ Arrange ]
        
        var logradouro = "asdf".PadLeft(303, 'w');

        // [ Act ]


        var resultado = _validacoesBuilder
            .Campo(logradouro)
            .NaoPodeSerNulo()
            .Minimo(2)
            .Maximo(300)
            .Validar();
        
        // [ Assert ]

        resultado.IsFailed
            .Should()
            .BeTrue();

        var erros = resultado.Errors
            .Select(x => x.Message);

        erros
            .Should()
            .Contain($"O campo {nameof(logradouro)} não pode ter mais de {300} caracteares");

    }
}