using System.Net;
using FluentResults;

namespace Core;

public class FieldError : Error
{
    public FieldError(string message, HttpStatusCode code = HttpStatusCode.BadRequest) : base(message)
    {
        Metadata.Add("ErrorCode", (int)code);
    }
}