using System.Net;
using FluentResults;

namespace Core;

public abstract class EntityBase<T>
{
    public Guid Id { get; init; }
    public DateTime DataHoraCadastro { get; }
    public DateTime DataHoraAlteracao { get; private set; }
    public bool Ativo { get; private set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        DataHoraCadastro = DateTime.Now;
    }

    public abstract Result<T> Validar();

    public void HouveAtualizacao() => DataHoraAlteracao = DateTime.Now;

    public void Desativar()
    {
        Ativo = false;
        HouveAtualizacao();
    }

    public void Ativar()
    {
        Ativo = true;
        HouveAtualizacao();
    }

    protected static Result FailIf(
        bool isFailure,
        string message,
        HttpStatusCode code = HttpStatusCode.BadRequest
    ) => Result.FailIf(isFailure, new FieldError(message, code));


    public override bool Equals(object? obj)
    {
        var compareTo = obj as EntityBase<T>;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(EntityBase<T> a, EntityBase<T> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityBase<T> a, EntityBase<T> b) 
        => !(a == b);

    public override int GetHashCode() 
        => (GetType().GetHashCode() * 907) + Id.GetHashCode();

    public override string ToString() 
        => $"{GetType().Name} [Id={Id}]";
}