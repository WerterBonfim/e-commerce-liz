using Application.LidarComProduto.Commands;
using Domain.Entities;
using Domain.Ports;
using Microsoft.Extensions.Logging;


namespace UnitTest.ApplicationTests.CommandHandlers;

public class ProdutoCommandHandlerTests
{
    [Fact(DisplayName = "Deve incluir um novo produto com sucesso")]
    [Trait("Core > Application > Produto > Handler", "Incluir")]
    public async Task DeveIncluirNovoProduto()
    {
        // [ Arrage ]

        var logger = A.Fake<ILogger<IncluirProdutoCommandHandler>>();
        var repositorio = A.Fake<IProdutoRepository>();
        

        var incluirProduto = new IncluirProdutoCommandHandler(
            logger,
            repositorio
            );

        var command = new IncluirProdutoCommand(
            "Guitarra",
            "Guitarra Gibson Les Paul", 
            14000,
            "Instrumento"
            );
        // [ Act ]

        var resultado = await incluirProduto.Handle(command, CancellationToken.None);

        // [ Assert ]

        resultado.IsSuccess
            .Should()
            .BeTrue("Comando foi montado corretamente");

        A.CallTo(() => repositorio.InserirAsync(A<Produto>._, CancellationToken.None))
            .MustHaveHappenedANumberOfTimesMatching(x => x == 1);

        A.CallTo(logger)
            .Where(call => call.Method.Name == "Log")
            .MustHaveHappened(1, Times.Exactly);
        
    }


    [Fact(DisplayName = "Não deveria incluir produto, um cancelationToken foi acionado")]
    [Trait("Core > Application > Produto > Handler", "Incluir")]
    public async Task NaoDeveriaIncluirProdutoCancelationTokenAcionado()
    {
        // [ Arrage ]

        var logger = A.Fake<ILogger<IncluirProdutoCommandHandler>>();
        var repositorio = A.Fake<IProdutoRepository>();
        

        var incluirProduto = new IncluirProdutoCommandHandler(
            logger,
            repositorio
        );

        var command = new IncluirProdutoCommand(
            "Guitarra",
            "Guitarra Gibson Les Paul", 
            14000,
            "Instrumento"
        );

        var token = new CancellationToken(true);
        
        // [ Act ]

        var resultado = await incluirProduto.Handle(command, token);

        // [ Assert ]

        resultado.IsFailed
            .Should()
            .BeTrue("Comando foi montado corretamente");

        A.CallTo(() => repositorio.InserirAsync(A<Produto>._, token))
           .MustNotHaveHappened();

        A.CallTo(logger)
            .Where(call => call.Method.Name == "Log")
            .MustHaveHappened(1, Times.Exactly);
    }
    
    
    [Fact(DisplayName = "Não deveria incluir produto, command totamente inválido")]
    [Trait("Core > Application > Produto > Handler", "Incluir")]
    public async Task NaoDeveriaIncluirProdutoCommandTotalmenteInvalido()
    {
        // [ Arrage ]

        var logger = A.Fake<ILogger<IncluirProdutoCommandHandler>>();
        var repositorio = A.Fake<IProdutoRepository>();
        

        var incluirProduto = new IncluirProdutoCommandHandler(
            logger,
            repositorio
        );

        var command = new IncluirProdutoCommand(
            "",
            "", 
            -14000,
            ""
        );

        // [ Act ]

        var resultado = await incluirProduto.Handle(command, CancellationToken.None);

        // [ Assert ]

        resultado.IsFailed
            .Should()
            .BeTrue("O command está totalmente inválido");

        A.CallTo(() => repositorio.InserirAsync(A<Produto>._, CancellationToken.None))
            .MustNotHaveHappened();

        A.CallTo(logger)
            .Where(call => call.Method.Name == "Log")
            .MustHaveHappened(1, Times.Exactly);
    }
    
    
    


}