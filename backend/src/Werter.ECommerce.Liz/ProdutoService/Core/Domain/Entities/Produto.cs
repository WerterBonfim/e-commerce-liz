using System.Net;
using Core;
using FluentResults;

namespace Domain.Entities;

public class Produto : EntityBase
{
    public string? Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int QuantidadeEmEstoque { get; private set; }
    private IList<string> _imagens = new List<string>(10);

    public IReadOnlyCollection<string> Imagens => (IReadOnlyCollection<string>)_imagens;

    public string Categorias { get; private set; }

    public Produto()
    {
    }

    public Result IncluirImagem(string nomeImagem)
    {
        var resultado = Result.Merge(
            Result.FailIf(string.IsNullOrEmpty(nomeImagem), MensagemDeErroPara.NomeDaImagemInvalida),
            Result.FailIf(Imagens.Count >= 10, MensagemDeErroPara.QuantidadeMaximaDeImagensExedida)
        );

        if (resultado.IsFailed)
            return resultado;

        _imagens.Add(nomeImagem);
        HouveAtualizacao();

        return Result.Ok();
    }
    

    public static Result<Produto> Criar(
        string? nome, 
        string descricao, 
        decimal preco, 
        string categoria)
    {
        var resultadoValidacao = Result.Merge(
            VerificaSeNomeValido(nome),
            VerificaSeDescricaoEValida(descricao),
            VerificaSePrecoEValida(preco),
            VerificaSeCategoriaEValida(categoria)
        );

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        return Result.Ok(new Produto
        {
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Categorias = categoria
        });
    }

    public Result AlterarNome(string nome)
    {
        var resultado = VerificaSeNomeValido(nome);
        if (resultado.IsFailed)
            return resultado;

        Nome = nome;
        HouveAtualizacao();

        return Result.Ok();
    }
    
    public Result AlterarPreco(decimal preco)
    {
        var resultado = VerificaSePrecoEValida(preco);
        if (resultado.IsFailed)
            return resultado;

        Preco = preco;
        HouveAtualizacao();

        return Result.Ok();
    }
    
    public Result AlterarDescricao(string descricao)
    {
        var resultado = VerificaSeDescricaoEValida(descricao);
        if (resultado.IsFailed)
            return resultado;

        Descricao = descricao;
        HouveAtualizacao();

        return Result.Ok();
    }
    
    public Result AlterarQuantidadeEmEstoque(int qtdEmEstoque)
    {
        var resultado = VerificaSeQuantidadeEmEstoqueEValida(qtdEmEstoque);
        if (resultado.IsFailed)
            return resultado;

        QuantidadeEmEstoque = qtdEmEstoque;
        HouveAtualizacao();

        return Result.Ok();
    }

    public Result AlterarCategoria(string categoria)
    {
        var resultado = VerificaSeCategoriaEValida(categoria);
        if (resultado.IsFailed)
            return resultado;

        Categorias = categoria;
        HouveAtualizacao();

        return Result.Ok();
    }

    private static Result FailIf(bool isFailure, string message, HttpStatusCode code = HttpStatusCode.BadRequest)
        => Result.FailIf(isFailure, new FieldError(message, (int)code));

    private static Result VerificaSeNomeValido(string? nome) =>
        Result.Merge(
            FailIf(string.IsNullOrEmpty(nome), MensagemDeErroPara.NomeInvalido),
            FailIf(nome?.Length > 50, MensagemDeErroPara.NomeComMaisDe50Caracteres),
            FailIf(nome?.Length < 2, MensagemDeErroPara.NomeComMenosDe2Caracteres)
            
        );

    private static Result VerificaSeDescricaoEValida(string? descricao) =>
        Result.Merge(
            FailIf(string.IsNullOrEmpty(descricao), MensagemDeErroPara.DescricaoInvalida),
            FailIf(descricao?.Length > 300, MensagemDeErroPara.NomeComMaisDe300Caracteres)
        );


    private static Result VerificaSePrecoEValida(decimal preco) =>
        Result.Merge(
            FailIf(preco == 0, MensagemDeErroPara.PrecoZerado),
            FailIf(preco < 0, MensagemDeErroPara.PrecoNegativo)
        );

    private static Result VerificaSeCategoriaEValida(string categoria) =>
        Result.Merge(
            FailIf(string.IsNullOrEmpty(categoria), MensagemDeErroPara.CategoriaNaoInformada),
            FailIf(categoria?.Length > 50, MensagemDeErroPara.CategoriaComMaisDe50Caracteres)
        );
    
    private static Result VerificaSeQuantidadeEmEstoqueEValida(int qtd) =>
        Result.Merge(
            FailIf(qtd < 0, MensagemDeErroPara.QuantidadeEmEstoqueInvalida)
        );

    public struct MensagemDeErroPara
    {
        public const string NomeInvalido = "O nome do produto não foi informado";
        public const string NomeComMaisDe50Caracteres = "O nome do produto não pode ter mais de 50 caracteres";
        public const string NomeComMenosDe2Caracteres = "O nome do produto não pode ter menos de 2 caracteres";

        public const string DescricaoInvalida = "A descrição do produto não foi preenchida";
        public const string NomeComMaisDe300Caracteres = "A descrição do produto não pode ter mais de 300 caracteres";

        public const string PrecoZerado = "O preço não foi informado";
        public const string PrecoNegativo = "O preço informado é inválido";

        public const string CategoriaNaoInformada = "O campo categoria é um campo obrigatório";
        public const string CategoriaComMaisDe50Caracteres = "O campo categoria tem mais de 50 caracteres";

        public const string NomeDaImagemInvalida = "Nome da imagem está inválida";

        public const string QuantidadeMaximaDeImagensExedida =
            "Quantidade de imagens excedida para esse produto. A quantidade máxima é 10.";
        
        public const string QuantidadeEmEstoqueInvalida = "O valor da quantidade em estoque está inválida";

    }
}