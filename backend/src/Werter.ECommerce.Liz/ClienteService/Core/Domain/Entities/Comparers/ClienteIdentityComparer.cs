namespace Domain.Entities.Comparers;

public record ClienteIdentityComparer : IEqualityComparer<Cliente>
{
    public bool Equals(Cliente? x, Cliente? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(null, y)) return false;
        if (ReferenceEquals(null, x)) return false;

        return x.Cpf == y.Cpf;
    }

    public int GetHashCode(Cliente obj)
    {
        var hashCode = obj.Cpf.GetHashCode();
        return hashCode;
    }
}

