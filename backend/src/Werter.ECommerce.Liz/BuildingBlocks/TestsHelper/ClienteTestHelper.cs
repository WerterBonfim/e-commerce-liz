using Domain.Entities;

namespace TestsHelper;

public class ClienteTestHelper
{
    public static Cliente MontarClienteFake()
    {
        return new Cliente
        {
            Cpf = "12312312345",
            Endereco = new Endereco
            {
                Bairro = "Algum Bairro",
                Cep = "12312123",
                Cidade = "São Paulo",
                Estado = "SP",
                Complemento = "Casa",
                Logradouro = "Rua em algum lugar",
                Numero = "222"
            },
            Rg = "12311234",
            Nome = "Fulano de tal"
        };
    }
}