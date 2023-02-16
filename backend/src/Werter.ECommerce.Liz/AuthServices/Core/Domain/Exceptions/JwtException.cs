namespace Domain.Exceptions;

[Serializable]
public class JwtException : Exception
{
    public JwtException(string? message) : base(message)
    {
    }

    public JwtException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}