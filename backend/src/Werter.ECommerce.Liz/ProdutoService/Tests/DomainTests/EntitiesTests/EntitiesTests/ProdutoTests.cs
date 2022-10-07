using Domain.Entities;
using FluentAssertions;
using FluentResults;

namespace EntitiesTests;

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
            CriarCenariosParaDescricao("a".PadLeft(301,'a'));

        // [ Assert ]

        resultadoAoCriarProduto.IsFailed
            .Should().BeTrue("Descrição com mais de 300 caracteres");

        resultadoAoCriarProduto
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

    #endregion


    #region [ Validação para Imagens ]

    [Fact(DisplayName = "Não deveria incluir produto, imagem com nome vazio")]
    [Trait("Core > Domain > Entities > Produto", "Imagem")]
    public void NaoDeveriaIncluirProdutoImagemComNomeVazio()
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

    #endregion
    
    private Result<Produto> CriarCenariosParaNome(string? nome) =>
        Produto
            .Criar(
                nome,
                "Descrição",
                10,
                "Instrumento Musical");
    
    private Result<Produto> CriarCenariosParaDescricao(string? descricao) =>
        Produto
            .Criar(
                "Nome valido",
                descricao,
                10,
                "Instrumento Musical");
    
    private Result<Produto> CriarCenariosParaPreco(decimal preco) =>
        Produto
            .Criar(
                "Nome valido",
                "descricao",
                preco,
                "Instrumento Musical");
    
    
    private Result<Produto> CriarCenariosParaCategoria(string categoria) =>
        Produto
            .Criar(
                "Nome valido",
                "descricao",
                300,
                categoria);
}