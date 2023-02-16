using FluentResults;

namespace TestsHelper;

public static class FluentResultExtensions
{
    public static string? MessageError<T>(this Result<T> result)
        => result.Errors.FirstOrDefault()?.Message;
    
    public static string? MessageError(this Result result)
        => result.Errors.FirstOrDefault()?.Message;
}