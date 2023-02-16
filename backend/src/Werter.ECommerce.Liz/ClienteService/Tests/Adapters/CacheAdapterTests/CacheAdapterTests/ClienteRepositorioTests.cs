using Data;
using Microsoft.Extensions.Logging;
using TestsHelper;

namespace CacheAdapterTests;

public class ClienteRepositorioTests
{

    [Fact(DisplayName = "Deveria cadastrar um novo cliente")]
    [Trait("Adapter > CacheAdapter > ClienteRepositorio", "InserirAsync")]
    public async Task DeveriaCadastrarUmNovoClienteComSucesso()
    {
        // [ Arrange ]

        var logger = A.Fake<ILogger<ClienteRepositorio>>();

        var repositorio = new ClienteRepositorio(logger);
        var cliente = ClienteTestHelper.MontarClienteFake();

        var resultadoEsperado = Result.Ok();

        // [ Act ]

        var resultado = await repositorio.InserirAsync(cliente, CancellationToken.None);


        // [ Assert ]

        resultado.IsSuccess
            .Should()
            .Be(resultadoEsperado.IsSuccess, resultado.Errors.FirstOrDefault()?.Message);
    }


    [Fact(DisplayName = "Deveria notificar que cliente já existe")]
    [Trait("Adapter > CacheAdapter > ClienteRepositorio", "InserirAsync")]
    public async Task DeveriaFazerAlgo()
    {
        // [ Arrange ]

        var logger = A.Fake<ILogger<ClienteRepositorio>>();

        var repositorio = new ClienteRepositorio(logger);
        var cliente = ClienteTestHelper.MontarClienteFake();

        var resultadoEsperado = Result.Fail("Esse cliente já cadastrado");

        // [ Act ]

        await repositorio.InserirAsync(cliente, CancellationToken.None);
        var resultado = await repositorio.InserirAsync(cliente, CancellationToken.None);


        // [ Assert ]

        resultado.IsFailed
            .Should()
            .Be(resultadoEsperado.IsFailed);

        resultado.Errors.First().Message
            .Should()
            .Be(resultadoEsperado.Errors.First().Message);
    }

    
}