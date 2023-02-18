using System.ComponentModel.DataAnnotations;
using System.Net;
using Core;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities;

public class Produto : EntityBase<Produto>
{
    [StringLength(50)]
    public string? Nome { get; set; }
    
    [StringLength(500)]
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int QuantidadeEmEstoque { get; set; }
    private IList<string> _imagens = new List<string>(10);

    public IReadOnlyCollection<string> Imagens => (IReadOnlyCollection<string>)_imagens;

    public string Categorias { get; set; }

    public Produto()
    {
    }

    public Result IncluirImagem(string nomeImagem)
    {
        var resultado = Result.Merge(
            Result.FailIf(string.IsNullOrEmpty(nomeImagem), MensagemDeErro.CampoInvalido("NomeImagem")),
            Result.FailIf(Imagens.Count >= 10, 
                "Quantidade de imagens excedida para esse produto. A quantidade máxima é 10.")
        );

        if (resultado.IsFailed)
            return resultado;

        _imagens.Add(nomeImagem);
        HouveAtualizacao();

        return Result.Ok();
    }


    public override Result Validar()
    {
        var resultado = new Validacao().Validate(this);
        
        return resultado.IsValid is false
            ? Result.Fail(resultado.Errors.Select(x => x.ErrorMessage)) 
            : Result.Ok();
    }

    public Result AlterarNome(string nome)
    {
        Nome = nome;
        
        var resultadoValidacao = Validar();
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        HouveAtualizacao();

        return Result.Ok();
    }
    
    public Result AlterarPreco(decimal preco)
    {
        Preco = preco;
        
        var resultadoValidacao = Validar();
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;
        
        HouveAtualizacao();

        return Result.Ok();
    }
    
    public Result AlterarDescricao(string descricao)
    {
        Descricao = descricao;
        
        var resultadoValidacao = Validar();
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;
        
        HouveAtualizacao();

        return Result.Ok();
    }
    
    public Result AlterarQuantidadeEmEstoque(int qtdEmEstoque)
    {
        QuantidadeEmEstoque = qtdEmEstoque;
        
        var resultadoValidacao = Validar();
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;
        
        HouveAtualizacao();
        return Result.Ok();
    }


    public Result AlterarCategoria(string categoria)
    {
        Categorias = categoria;
        
        var resultadoValidacao = Validar();
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;
        
        HouveAtualizacao();

        return Result.Ok();
    }
    
    private class Validacao : AbstractValidator<Produto>
    {
        public Validacao()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Nome)))
                .MaximumLength(50)
                .WithMessage(MensagemDeErro.ExcedeuMaximoDeCaracteres(nameof(Nome), 50));

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(MensagemDeErro.CampoInvalido(nameof(Nome)))
                .MaximumLength(500)
                .WithMessage(MensagemDeErro.ExcedeuMaximoDeCaracteres(nameof(Descricao), 500));

            RuleFor(x => x.Preco)
                .GreaterThan(0)
                .WithMessage("O preço do produto deve ser maior que zero.");

            RuleFor(x => x.QuantidadeEmEstoque)
                .GreaterThanOrEqualTo(0)
                .WithMessage("A quantidade em estoque do produto deve ser maior ou igual a zero.");

            RuleFor(x => x.Categorias)
                .NotEmpty().WithMessage("O ID da categoria do produto não pode estar vazio.")
                .MaximumLength(50)
                .WithMessage(MensagemDeErro.ExcedeuMaximoDeCaracteres(nameof(Categorias), 50));
        }
    }
}