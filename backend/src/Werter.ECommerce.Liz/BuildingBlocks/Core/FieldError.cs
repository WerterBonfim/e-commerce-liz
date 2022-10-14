using FluentResults;

namespace Core;

public class FieldError : Error
{
    public FieldError(string message, int code) : base(message)
    {
        Metadata.Add("ErrorCode", code);
    }
}