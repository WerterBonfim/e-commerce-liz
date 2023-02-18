using AutoFixture;
using AutoFixture.DataAnnotations;
using Core;
using Domain.Entities;

namespace UnitTest.DomainTests;

public class ProdutoTests
{
    #region [ Validação do nome ]

    [Fact(DisplayName = "Não deveria incluir produto sem nome")]
    [Trait("Core > Domain > Entities > Produto", "Nome")]
    public void NaoDeveriaIncluirProdutoSemNome()
    {
        // [ Act ]

        var resultadoAoCriarProduto = CriarCenariosParaNome("");

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Nome é inválido");

        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains(MensagemDeErro.CampoInvalido("Nome")));
    }

    [Fact(DisplayName = "Não deveria incluir produto com nome null")]
    [Trait("Core > Domain > Entities > Produto", "Nome")]
    public void NaoDeveriaIncluirProdutoComNomeNull()
    {
        // [ Arrange ]

        var resultadoAoCriarProduto = CriarCenariosParaNome(null);

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Nome é inválido");

        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains(MensagemDeErro.CampoInvalido("Nome")));
    }


    [Fact(DisplayName = "Não deveria incluir produto, nome tem mais de 50 caracteres")]
    [Trait("Core > Domain > Entities > Produto", "Nome")]
    public void NaoDeveriaIncluirProdutoNomeComMaisDe50Caracteres()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaNome("a".PadLeft(51, 'a'));

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Nome tem mais de 50 caracteres");

        var nomeComMuitosCaracteres = MensagemDeErro.ExcedeuMaximoDeCaracteres("Nome", 50);
        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains(nomeComMuitosCaracteres));
    }


    // [ Alterações ]

    [Fact(DisplayName = "Não deveria alterar produto, produto sem nome")]
    [Trait("Core > Domain > Entities > Produto - alteração", "Nome")]
    public void NaoDeveriaAlterarProdutoSemNome()
    {
        // [ Arrange ]

        var produto = CriarProduto();

        // [ Act ]

        var resultado = produto.AlterarNome("");

        // [ Assert ]

        resultado.IsFailed
            .Should().BeTrue("Nome é inválido");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message.Contains(MensagemDeErro.CampoInvalido("Nome")));
    }

    [Fact(DisplayName = "Não deveria alterar produto, nome tem mais de 50 caracteres")]
    [Trait("Core > Domain > Entities > Produto - alteração", "Nome")]
    public void NaoDeveriaAlterarProdutoNomeComMaisDe50Caracteres()
    {
        // [ Arrange ]

        var produto = CriarProduto();

        // [ Act ]

        var resultado = produto.AlterarNome("a".PadLeft(51, 'a'));

        // [ Assert ]

        resultado.IsFailed
            .Should().BeTrue("Nome tem mais de 50 caracteres");

        var nomeComMuitosCaracteres = MensagemDeErro.ExcedeuMaximoDeCaracteres("Nome", 50);
        resultado
            .HasError(x => x.Message.Contains(nomeComMuitosCaracteres));
    }

    #endregion

    #region [ Validação da descrição ]

    [Fact(DisplayName = "Não deveria incluir produto, sem descrição")]
    [Trait("Core > Domain > Entities > Produto", "Descricao")]
    public void NaoDeveriaIncluirProdutoSemDescricao()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaDescricao("");

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Descrição em branco");

        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains(MensagemDeErro.CampoInvalido("Descricao")));
    }

    [Fact(DisplayName = "Não deveria incluir produto, descrição nulla")]
    [Trait("Core > Domain > Entities > Produto", "Descricao")]
    public void NaoDeveriaIncluirProdutoDescricaoNull()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaDescricao(null);

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Descrição com valor null");

        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains(MensagemDeErro.CampoInvalido("Descricao")));
    }


    [Fact(DisplayName = "Não deveria incluir produto, descrição com mais de 500 caracteres")]
    [Trait("Core > Domain > Entities > Produto", "Descricao")]
    public void NaoDeveriaIncluirProdutoDescricaoComMaisDe300Caracteres()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaDescricao("a".PadLeft(501, 'a'));

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Descrição com mais de 300 caracteres");

        var descricaoMuitoGrande = MensagemDeErro
            .ExcedeuMaximoDeCaracteres("Descricao", 500);
        
        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains(descricaoMuitoGrande));
    }


    // Alteração

    [Fact(DisplayName = "Não deveria alterar produto, sem descrição")]
    [Trait("Core > Domain > Entities > Produto - Alteração", "Descricao")]
    public void NaoDeveriaAlterarProdutoSemDescricao()
    {
        // [ Act ]
        var produto = CriarProduto();
        var resultado = produto.AlterarDescricao("");

        // [ Assert ]

        resultado.IsFailed
            .Should().BeTrue("Descrição em branco");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message.Contains(MensagemDeErro.CampoInvalido("Descricao")));
    }


    [Fact(DisplayName = "Não deveria alterar produto, descrição com mais de 500 caracteres")]
    [Trait("Core > Domain > Entities > Produto - Alteração", "Descricao")]
    public void NaoDeveriaAlterarProdutoDescricaoComMaisDe300Caracteres()
    {
        // [ Act ]

        var produto = CriarProduto();
        var resultado = produto.AlterarDescricao("a".PadLeft(501, 'a'));

        // [ Assert ]

        resultado.IsFailed
            .Should().BeTrue("Descrição com mais de 300 caracteres");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message.Contains(MensagemDeErro.CampoInvalido("Descricao")));
    }

    #endregion

    #region [ Validação de Preço ]

    [Fact(DisplayName = "Não deveria incluir produto, preço zerado")]
    [Trait("Core > Domain > Entities > Produto", "Preço")]
    public void NaoDeveriaIncluirProdutoPrecoZerado()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaPreco(0);

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Preço zerado");

        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains("O preço do produto deve ser maior que zero."));
    }


    [Fact(DisplayName = "Não deveria incluir produto, preço negativo")]
    [Trait("Core > Domain > Entities > Produto", "Preço")]
    public void NaoDeveriaIncluirProdutoPrecoNegativo()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaPreco(-300);

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Preço negativo");

        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains("O preço do produto deve ser maior que zero."));
    }

    // Alteração

    [Fact(DisplayName = "Não deveria alterar produto, preço zerado")]
    [Trait("Core > Domain > Entities > Produto - Alteração", "Preço")]
    public void NaoDeveriaAlterarProdutoPrecoZerado()
    {
        // [ Arrage ]

        var produto = CriarProduto();

        // [ Act ]

        var resultado = produto.AlterarPreco(0);

        // [ Assert ]

        resultado.IsFailed
            .Should().BeTrue("Preço zerado");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message.Contains("O preço do produto deve ser maior que zero."));
    }


    [Fact(DisplayName = "Não deveria alterar produto, preço negativo")]
    [Trait("Core > Domain > Entities > Produto - Alteração", "Preço")]
    public void NaoDeveriaAlterarProdutoPrecoNegativo()
    {
        // [ Arrage ]

        var produto = CriarProduto();

        // [ Act ]

        var resultado = produto.AlterarPreco(-3000);

        // [ Assert ]

        resultado.IsFailed
            .Should().BeTrue("Preço negativo");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message.Contains("O preço do produto deve ser maior que zero."));
    }

    #endregion

    #region [ Validação da Categoria ]

    [Fact(DisplayName = "Não deveria incluir produto, categoria não informada")]
    [Trait("Core > Domain > Entities > Produto", "Categoria")]
    public void NaoDeveriaIncluirProdutoCategoriaNaoInformada()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaCategoria("");

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Categoria em branco");

        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains("O ID da categoria do produto não pode estar vazio."));
    }


    [Fact(DisplayName = "Não deveria incluir produto, categoria com mais de 50 caracteres")]
    [Trait("Core > Domain > Entities > Produto", "Categoria")]
    public void NaoDeveriaIncluirProdutoCategoriaComMaisDe50Caracteres()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaCategoria("a".PadLeft(51, 'a'));

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Categoria com mais de 50 caracteres");

        resultadoAoCriarProduto
            .HasError(x => x.Message.Contains(MensagemDeErro.ExcedeuMaximoDeCaracteres("Categoria", 50)));
    }


    // Cenarios de alteração

    [Fact(DisplayName = "Não deveria altear produto, categoria não informada")]
    [Trait("Core > Domain > Entities > Produto - Alteração", "Categoria")]
    public void NaoDeveriaAlterarProdutoCategoriaNaoInformada()
    {
        // [ Arrage ]

        var produto = CriarProduto();

        // [ Act ]

        var resultado = produto.AlterarCategoria("");


        // [ Assert ]

        resultado.IsFailed
            .Should().BeTrue("Categoria em branco");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message.Contains("O ID da categoria do produto não pode estar vazio."));
    }


    [Fact(DisplayName = "Não deveria altear produto, categoria com mais de 50 caracteres")]
    [Trait("Core > Domain > Entities > Produto - Alteração", "Categoria")]
    public void NaoDeveriaAlterarProdutoCategoriaComMaisDe50Caracteres()
    {
        // [ Arrage ]

        var produto = MontarProduto();

        // [ Act ]

        var resultado = produto.AlterarCategoria("a".PadLeft(51, 'a'));

        resultado.IsFailed
            .Should().BeTrue("Categoria com mais de 50 caracteres");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message.Contains(MensagemDeErro.ExcedeuMaximoDeCaracteres("Categoria", 50)));
    }

    private static Produto MontarProduto()
    {
        var fixture = new Fixture();
        var produto = fixture.Create<Produto>();
        return produto;
    }

    #endregion

    #region [ Validação para Imagens ]

    [Fact(DisplayName = "Deveria alterar produto, primeira imagem fornecida")]
    [Trait("Core > Domain > Entities > Produto - Alterar", "Imagem")]
    public void DeveriaAlterarProdutoPrimeiraImagemFornecida()
    {
        // [ Arrage ]

        var fixture = new Fixture();
        var produto = fixture.Create<Produto>();
        
        //var produto = CriarCenariosParaNome("Guitarra").Value;
        
        

        // [ Act ]

        var imagemIncluida = produto.IncluirImagem("guitarra.jpg");

        // [ Assert ]

        VerificarDataHoraAlteracao(produto.DataHoraAlteracao);

        imagemIncluida.IsFailed.Should()
            .BeFalse("Acabei de incluir a primeira imagem");
    }

    [Fact(DisplayName = "Deveria falhar ao alterar o produto, nome da imagem não foi fornecida")]
    [Trait("Core > Domain > Entities > Produto - Alterar", "Imagem")]
    public void DeveriaFalharAoAlterarProdutoNomeDaImagemNaoFoiFornecida()
    {
        // [ Arrage ]

        var fixture = new Fixture();
        var produto = fixture.Create<Produto>();

        // [ Act ]

        var imagemIncluida = produto.IncluirImagem("");

        // [ Assert ]

        imagemIncluida.IsFailed.Should()
            .BeTrue("Imagem sem nome");

        imagemIncluida
            .HasError(x => x.Message.Contains(MensagemDeErro.CampoInvalido("Imagem")));
    }

    [Fact(DisplayName = "Deveria falhar ao alterar o produto, produto já tem 10 imagens")]
    [Trait("Core > Domain > Entities > Produto - Alterar", "Imagem")]
    public void DeveriaFalharAoAlterarProdutoProdutoJaTem10Imagens()
    {
        // [ Arrage ]

        var fixture = new Fixture();
        var produto = fixture.Create<Produto>();

        // [ Act ]

        for (var indice = 0; indice < 10; indice++)
            produto.IncluirImagem($"imagem_{indice}.png");

        var imagemIncluida = produto.IncluirImagem("");

        // [ Assert ]

        imagemIncluida.IsFailed.Should()
            .BeTrue($"Tem {produto.Imagens.Count} produtos");

        imagemIncluida
            .HasError(x => 
                x.Message.Contains("Quantidade de imagens excedida para esse produto. A quantidade máxima é 10."))
            .Should().BeTrue($"Tem {produto.Imagens.Count} produtos");
    }

    #endregion

    #region [ Quantidade em estoque ]

    [Fact(DisplayName = "Não Deveria alterar produto, quantidade em estoque negativa")]
    [Trait("Core > Domain > Entities > Produto - Alterar", "QuantidadeEmEstoque")]
    public void NaoDeveriaAlterarProdutoQuantidadeEmEstoqueNegativa()
    {
        // [ Arrage ]

        var fixture = new Fixture();
        var produto = fixture.Create<Produto>();

        // [ Act ]

        var resultado = produto.AlterarQuantidadeEmEstoque(-1);

        // [ Assert ]

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado.IsFailed.Should()
            .BeTrue("Estoque negativo.");

        resultado
            .HasError(x => x.Message.Contains("A quantidade em estoque do produto deve ser maior ou igual a zero."));
    }


    [Fact(DisplayName = "Deveria alterar produto, quantidade em estoque valida")]
    [Trait("Core > Domain > Entities > Produto - Alterar", "QuantidadeEmEstoque")]
    public void DeveriaAlterarProdutoQuantidadeEmEstoqueValida()
    {
        // [ Arrage ]

        var produto = CriarProduto();

        // [ Act ]

        var resultado = produto.AlterarQuantidadeEmEstoque(10);

        // [ Assert ]

        VerificarDataHoraAlteracao(produto.DataHoraAlteracao);

        resultado.IsSuccess.Should()
            .BeTrue("Estoque válido");
    }

    #endregion

    #region [ Utils ]

    private Result<Produto> CriarCenariosParaNome(string? nome) =>
        new Produto
        {
            Nome = nome, 
            Descricao = "Descrição", 
            QuantidadeEmEstoque = 10, 
            Categorias = "Instrumento Musical",
            Preco = 10
        }.Validar();


    private Result<Produto> CriarCenariosParaDescricao(string? descricao) =>
        new Produto
        {
            Nome = "nome", 
            Descricao = descricao!, 
            QuantidadeEmEstoque = 10, 
            Categorias = "Instrumento Musical",
            Preco = 10
        }.Validar();

    private Result<Produto> CriarCenariosParaPreco(decimal preco) =>
        new Produto
        {
            Nome = "nome", 
            Descricao = "descricao", 
            QuantidadeEmEstoque = 10, 
            Categorias = "Instrumento Musical",
            Preco = preco
        }.Validar();


    private Result<Produto> CriarCenariosParaCategoria(string categoria) =>
        new Produto
        {
            Nome = "nome", 
            Descricao = "descricao", 
            QuantidadeEmEstoque = 10, 
            Categorias = categoria,
            Preco = 300
        }.Validar();

    private Produto CriarProduto() =>
        new()
        {
            Nome = "nome", 
            Descricao = "descricao", 
            QuantidadeEmEstoque = 10, 
            Categorias = "categoria",
            Preco = 300
        };


    private static void VerificarDataHoraAlteracao(DateTime dataHoraAlteracaoRegistrada)
    {
        dataHoraAlteracaoRegistrada
            .Should().BeAfter(DateTime.Now.Subtract(TimeSpan.FromMinutes(2)));

        dataHoraAlteracaoRegistrada
            .Should().BeBefore(DateTime.Now);
    }

    #endregion
}