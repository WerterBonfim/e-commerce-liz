using Application.Commands;
using Core.LogService;
using Domain.Ports;
using TestsHelper;

namespace ApplicationTests;

public class ClienteCommandHandlerTests
{
    [Fact(DisplayName = "Deveria adicionar um novo cliente com sucesso")]
    [Trait("Core > Application > CommandHandler", "IncluirClienteCommand")]
    public async Task DeveriaAdicionarClienteComSucesso()
    {
        // [ Arrange ]

        var repositorio = A.Fake<IClienteRepositorio>();

        A.CallTo(() => repositorio.ObterPorCpf(An<string>._, CancellationToken.None))
            .Returns(Result.Fail("Cliente não encontrado"));

        var logger = A.Fake<ILoggerManager>();

        var commandHandler = new ClienteCommandHandler(logger, repositorio);

        var resultadoEsperado = Result.Ok();

        var command = CriarCommand();

        // [ Act ]

        var resultado = await commandHandler.Handle(command, CancellationToken.None);


        // [ Assert ]

        var motivoErro = resultado.Errors
            .FirstOrDefault()
            ?.Message;

        resultado.IsSuccess
            .Should()
            .Be(resultadoEsperado.IsSuccess, motivoErro);
    }

    private static IncluirClienteCommand CriarCommand()
        => new(
            "Fulano de tal",
            "12312312312",
            "123123422",
            new IncluirEnderecoCommand(
                "Rua algum lugar por ai",
                "222",
                "01010110",
                "Bairro ali",
                "Casa",
                "São Paulo",
                "SP"
            )
        );

    [Fact(DisplayName = "Deveria notificar erro de CancellationToken")]
    [Trait("Core > Application > CommandHandler", "IncluirClienteCommand")]
    public async Task DeveriaNotificarFalhaDeCancellationToken()
    {
        // [ Arrange ]

        var logger = A.Fake<ILoggerManager>();
        var repositorio = A.Fake<IClienteRepositorio>();

        var commandHandler = new ClienteCommandHandler(logger, repositorio);

        var resultadoEsperado = Result.Fail("Requisição foi cancelada pelo cancellationToken");


        // [ Act ]

        var cancelation = new CancellationTokenSource();
        cancelation.Cancel();
        var resultado = await commandHandler.Handle(null, cancelation.Token);


        // [ Assert ]

        resultado.IsFailed
            .Should()
            .Be(resultadoEsperado.IsFailed);

        resultado.Errors.First().Message
            .Should()
            .Be(resultado.Errors.First().Message);
    }


    [Fact(DisplayName = "Deveria notificar erro, cliente já foi cadastrado")]
    [Trait("Core > Application > CommandHandler", "IncluirClienteCommand")]
    public async Task DeveriaNotificarErroClienteJaFoiCadastrado()
    {
        // [ Arrange ]

        var logger = A.Fake<ILoggerManager>();
        
        var repositorio = A.Fake<IClienteRepositorio>();
        var cliente = ClienteTestHelper.MontarClienteFake();


        A.CallTo(() => repositorio.ObterPorCpf(A<string>._, CancellationToken.None))
            .Returns(Result.Fail("Cliente não encontrado"))
            .Once()
            .Then
            .Returns(Result.Ok(cliente));
            

        var commandHandler = new ClienteCommandHandler(logger, repositorio);

        var resultadoEsperado = Result.Fail("Esse cliente já cadastrado");

        var command = CriarCommand();

        // [ Act ]

        await commandHandler.Handle(command, CancellationToken.None);
        var resultado = await commandHandler.Handle(command, CancellationToken.None);


        // [ Assert ]

        var motivoErro = resultado.MessageError();

        resultado.IsFailed
            .Should()
            .Be(resultadoEsperado.IsFailed, motivoErro);
    }
    
    [Fact(DisplayName = "Deveria notificar erro, cliente com todos os dados inválidos")]
    [Trait("Core > Application > CommandHandler", "IncluirClienteCommand")]
    public async Task DeveriaNotificarErroClienteComTodosDadosInvalidos()
    {
        // [ Arrange ]

        var logger = A.Fake<ILoggerManager>();
        
        var repositorio = A.Fake<IClienteRepositorio>();
        var cliente = ClienteTestHelper.MontarClienteFake();


        A.CallTo(() => repositorio.ObterPorCpf(A<string>._, CancellationToken.None))
            .Returns(Result.Fail("Cliente não encontrado"))
            .Once()
            .Then
            .Returns(Result.Ok(cliente));
            

        var commandHandler = new ClienteCommandHandler(logger, repositorio);

        var resultadoEsperado = Result.Fail("Esse cliente já cadastrado");

        var command = CriarCommand();

        // [ Act ]

        await commandHandler.Handle(command, CancellationToken.None);
        var resultado = await commandHandler.Handle(command, CancellationToken.None);


        // [ Assert ]

        var motivoErro = resultado.MessageError();

        resultado.IsFailed
            .Should()
            .Be(resultadoEsperado.IsFailed, motivoErro);
    }
}