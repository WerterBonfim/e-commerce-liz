using System.Net;
using Core;
using Core.LogService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CoreWebApi;


public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var feature = context.Features.Get<IExceptionHandlerFeature>();
                if (feature == null)
                    return;

                logger.LogError( $"Aconteceu algo de errado que não está certo. " +
                                 $"Error: {feature.Error.Message}");

                await context.Response.WriteAsync(new ErrorDto(
                        "Erro interno no servidor",
                        context.Response.StatusCode.ToString())
                    .ToJson());
            });
        });
    }
}