using Domain.Entities;
using TestsHelper;

namespace DomainTests;

public class TestandoCliente
{
    [Fact(DisplayName = "Deveria notificar erro, todas as propriedades estão inválidas")]
    [Trait("Core > Domain > Usuario", "Cliente")]
    public void DeveriaNotificarErroTodasPropriedadesInvalidas()
    {
        // [ Arrange ]

        var clienteInvalido = new Cliente
        {
            Cpf = "",
            Nome = "",
            Rg = "",
            Endereco = new Endereco
            {
                Bairro = "",
                Cidade = "",
                Cep = "",
                Complemento = "",
                Estado = "",
                Logradouro = "",
                Numero = ""
            }
        };

        // [ Act ]

        var resultado = clienteInvalido.Validar();

        // [ Assert ]

        resultado.IsFailed
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "Deveria notificar erro, endereço inválido")]
    [Trait("Core > Domain > Usuario", "Cliente")]
    public void DeveriaNotificarErroEnderecoInvalido()
    {
        // [ Arrange ]

        var clienteInvalido = new Cliente
        {
            Cpf = "12345612345",
            Nome = "asdfasd",
            Rg = "123456789",
            Endereco = new Endereco
            {
                Bairro = "",
                Cidade = "",
                Cep = "",
                Complemento = "",
                Estado = "",
                Logradouro = "",
                Numero = ""
            }
        };

        // [ Act ]

        var resultado = clienteInvalido.Validar();

        // [ Assert ]

        resultado.IsFailed
            .Should()
            .BeTrue();
    }
    
    
    [Fact(DisplayName = "Deveria notificar cliente valido")]
    [Trait("Core > Domain > Usuario", "Cliente")]
    public void DeveriaNotificarClienteValido()
    {
        // [ Arrange ]

        var clienteInvalido = new Cliente
        {
            Cpf = "40534295010",
            Nome = "asdfasd asdf",
            Rg = "203774954",
            Endereco = new Endereco
            {
                Bairro = "asdfasdf",
                Cidade = "asdfasd",
                Cep = "00000000",
                Complemento = "",
                Estado = "SP",
                Logradouro = "asdfas",
                Numero = "asdf"
            }
        };

        // [ Act ]

        var resultado = clienteInvalido.Validar();

        // [ Assert ]

        var mensagemDeErro = resultado.MessageError();

        resultado.IsSuccess
            .Should()
            .BeTrue(mensagemDeErro);
    }
}