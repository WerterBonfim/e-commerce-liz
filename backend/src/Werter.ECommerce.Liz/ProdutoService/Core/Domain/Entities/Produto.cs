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
            Result.FailIf(_imagens.Count == 10, MensagemDeErroPara.QuantidadeMaximaDeImagensExedida)
        );

        if (resultado.IsFailed)
            return resultado;

        _imagens.Add(nomeImagem);

        return Result.Ok();
    }

    public static Result<Produto> Criar(string? nome, string descricao, decimal preco, string categoria)
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

    private static Result VerificaSeNomeValido(string? nome) =>
        Result.Merge(
            Result.FailIf(string.IsNullOrEmpty(nome), MensagemDeErroPara.NomeInvalido),
            Result.FailIf(nome?.Length > 50, MensagemDeErroPara.NomeComMaisDe50Caracteres),
            Result.FailIf(nome?.Length < 2, MensagemDeErroPara.NomeComMenosDe2Caracteres)
        );

    private static Result VerificaSeDescricaoEValida(string? descricao) =>
        Result.Merge(
            Result.FailIf(string.IsNullOrEmpty(descricao), MensagemDeErroPara.DescricaoInvalida),
            Result.FailIf(descricao?.Length > 300, MensagemDeErroPara.NomeComMaisDe300Caracteres)
        );


    private static Result VerificaSePrecoEValida(decimal preco) =>
        Result.Merge(
            Result.FailIf(preco == 0, MensagemDeErroPara.PrecoZerado),
            Result.FailIf(preco < 0, MensagemDeErroPara.PrecoNegativo)
        );

    private static Result VerificaSeCategoriaEValida(string categoria) =>
        Result.Merge(
            Result.FailIf(string.IsNullOrEmpty(categoria), MensagemDeErroPara.CategoriaNaoInformada),
            Result.FailIf(categoria?.Length > 50, MensagemDeErroPara.CategoriaComMaisDe50Caracteres)
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

        public const string QuantidadeMaximaDeImagensExedida = "Não é possivel adicionar mais imagens";
    }
}