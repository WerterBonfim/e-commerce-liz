using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class InfraException : Exception
{
    protected InfraException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InfraException(string? message) : base(message)
    {
    }
}