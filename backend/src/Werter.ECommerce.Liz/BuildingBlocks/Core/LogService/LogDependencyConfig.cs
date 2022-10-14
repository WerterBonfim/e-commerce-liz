using Core.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace Core.LogService;

public static class LogDependencyConfig
{
    public static void AddLogManager(this IServiceCollection services, IConfiguration configuration)
    {
        var pathLog = configuration?.GetSection("NLog")?["LoggingConfigurationPath"];
        if (string.IsNullOrEmpty(pathLog))
            throw new InfraException("O Caminho do arquivo de configuração do log não foi definido no appsettings");

        LogManager.LoadConfiguration(pathLog);
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}