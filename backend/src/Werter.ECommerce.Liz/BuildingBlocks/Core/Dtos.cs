using System.Text.Json;
using FluentResults;

namespace Core;

public record ErrorDto(string Message, string Code);
public record ErrorDevDto(string Message, string Code, string InternalMessage);

public record ResultDto(bool IsSuccess, IEnumerable<ErrorDto> Errors);

public static class ResultDtoExtensions
{
    public static string ToJson(this ErrorDto errorDto) =>
        JsonSerializer.Serialize(errorDto);

    public static ResultDto ToResultDto(this Result result)
    {
        return result.IsSuccess
            ? new ResultDto(true, Enumerable.Empty<ErrorDto>())
            : new ResultDto(false, TranformErrors(result.Errors));
    }

    public static ResultDto ToResultDto<T>(this Result<T> result)
    {
        return result.IsSuccess
            ? new ResultDto(true, Enumerable.Empty<ErrorDto>())
            : new ResultDto(false, TranformErrors(result.Errors));
    }

    private static IEnumerable<ErrorDto> TranformErrors(List<IError> errors)
        => errors.Select(TranformError);

    private static ErrorDto TranformError(IError error)
    {
        var errorCode = TransformErrorCode(error);
        return new ErrorDto(error.Message, errorCode);
    }

    private static string TransformErrorCode(IError error)
    {
        if (error.Metadata.TryGetValue("ErrorCode", out var errorCode))
            return errorCode.ToString() ?? string.Empty;

        return "";
    }
}