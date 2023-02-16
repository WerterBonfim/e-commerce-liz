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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.NomeInvalido);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.NomeInvalido);
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

        resultadoAoCriarProduto
            .HasError(x => x.Message == Produto.MensagemDeErroPara.NomeComMaisDe50Caracteres);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.NomeInvalido);
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

        resultado
            .HasError(x => x.Message == Produto.MensagemDeErroPara.NomeComMaisDe50Caracteres);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.DescricaoInvalida);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.DescricaoInvalida);
    }


    [Fact(DisplayName = "Não deveria incluir produto, descrição com mais de 300 caracteres")]
    [Trait("Core > Domain > Entities > Produto", "Descricao")]
    public void NaoDeveriaIncluirProdutoDescricaoComMaisDe300Caracteres()
    {
        // [ Act ]

        var resultadoAoCriarProduto =
            CriarCenariosParaDescricao("a".PadLeft(301, 'a'));

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Descrição com mais de 300 caracteres");

        resultadoAoCriarProduto
            .HasError(x => x.Message == Produto.MensagemDeErroPara.DescricaoInvalida);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.DescricaoInvalida);
    }


    [Fact(DisplayName = "Não deveria alterar produto, descrição com mais de 300 caracteres")]
    [Trait("Core > Domain > Entities > Produto - Alteração", "Descricao")]
    public void NaoDeveriaAlterarProdutoDescricaoComMaisDe300Caracteres()
    {
        // [ Act ]

        var produto = CriarProduto();
        var resultado = produto.AlterarDescricao("a".PadLeft(301, 'a'));

        // [ Assert ]

        resultado.IsFailed
            .Should().BeTrue("Descrição com mais de 300 caracteres");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message == Produto.MensagemDeErroPara.DescricaoInvalida);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.PrecoZerado);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.PrecoNegativo);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.PrecoZerado);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.PrecoNegativo);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.CategoriaNaoInformada);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.CategoriaComMaisDe50Caracteres);
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
            .HasError(x => x.Message == Produto.MensagemDeErroPara.CategoriaNaoInformada);
    }


    [Fact(DisplayName = "Não deveria altear produto, categoria com mais de 50 caracteres")]
    [Trait("Core > Domain > Entities > Produto - Alteração", "Categoria")]
    public void NaoDeveriaAlterarProdutoCategoriaComMaisDe50Caracteres()
    {
        // [ Arrage ]

        var produto = CriarProduto();

        // [ Act ]

        var resultado = produto.AlterarCategoria("a".PadLeft(51, 'a'));

        resultado.IsFailed
            .Should().BeTrue("Categoria com mais de 50 caracteres");

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado
            .HasError(x => x.Message == Produto.MensagemDeErroPara.CategoriaComMaisDe50Caracteres);
    }

    #endregion

    #region [ Validação para Imagens ]

    [Fact(DisplayName = "Deveria alterar produto, primeira imagem fornecida")]
    [Trait("Core > Domain > Entities > Produto - Alterar", "Imagem")]
    public void DeveriaAlterarProdutoPrimeiraImagemFornecida()
    {
        // [ Arrage ]

        var produto = CriarCenariosParaNome("Guitarra").Value;

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

        var produto = CriarCenariosParaNome("Guitarra").Value;

        // [ Act ]

        var imagemIncluida = produto.IncluirImagem("");

        // [ Assert ]

        imagemIncluida.IsFailed.Should()
            .BeTrue("Imagem sem nome");

        imagemIncluida.HasError(x => x.Message == Produto.MensagemDeErroPara.NomeDaImagemInvalida);
    }

    [Fact(DisplayName = "Deveria falhar ao alterar o produto, produto já tem 10 imagens")]
    [Trait("Core > Domain > Entities > Produto - Alterar", "Imagem")]
    public void DeveriaFalharAoAlterarProdutoProdutoJaTem10Imagens()
    {
        // [ Arrage ]

        var produto = CriarCenariosParaNome("Guitarra").Value;

        // [ Act ]

        for (var indice = 0; indice < 10; indice++)
            produto.IncluirImagem($"imagem_{indice}.png");

        var imagemIncluida = produto.IncluirImagem("");

        // [ Assert ]

        imagemIncluida.IsFailed.Should()
            .BeTrue($"Tem {produto.Imagens.Count} produtos");

        imagemIncluida
            .HasError(x => x.Message == Produto.MensagemDeErroPara.QuantidadeMaximaDeImagensExedida)
            .Should().BeTrue($"Tem {produto.Imagens.Count} produtos");
    }

    #endregion

    #region [ Quantidade em estoque ]

    [Fact(DisplayName = "Não Deveria alterar produto, quantidade em estoque negativa")]
    [Trait("Core > Domain > Entities > Produto - Alterar", "QuantidadeEmEstoque")]
    public void NaoDeveriaAlterarProdutoQuantidadeEmEstoqueNegativa()
    {
        // [ Arrage ]

        var produto = CriarProduto();

        // [ Act ]

        var resultado = produto.AlterarQuantidadeEmEstoque(-1);

        // [ Assert ]

        produto.DataHoraAlteracao
            .Should().Be(DateTime.MinValue);

        resultado.IsFailed.Should()
            .BeTrue("Estoque negativo.");

        resultado
            .HasError(x => x.Message == Produto.MensagemDeErroPara.QuantidadeEmEstoqueInvalida);
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