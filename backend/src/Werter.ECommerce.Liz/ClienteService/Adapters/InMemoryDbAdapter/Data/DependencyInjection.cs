using Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyInjection
{
    public static void UseCacheAdapter(this IServiceCollection services)
    {
        services.AddSingleton<IClienteRepositorio, ClienteRepositorio>();
    }
}