using Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RepoDb.Options;

namespace Data;

public static class RepoDbDependencyConfig
{
    public static void AddRepoDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        RepoDb.GlobalConfiguration.Setup();

        var connectionStringRepoDb = configuration.GetConnectionString("RepoDb");
        if (connectionStringRepoDb == null)
            throw new ArgumentNullException("configuration.GetConnectionString(\"x\")");

        services.AddTransient<IProdutoRepository, ProdutoRepositorio>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<ProdutoRepositorio>>();
            return new ProdutoRepositorio(connectionStringRepoDb, logger);
        });
    }
}