using Application.Commands;
using Application.Query;
using Core.LogService;
using Data;
using MediatR;

namespace Api.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddScoped<, >();
        // services.AddScoped<>();
        
        services.AddLogManager(configuration);

        // Mediator
        services.AddMediatR(x => 
            x.RegisterServicesFromAssembly( typeof(ClienteCommandHandler).Assembly));

        services.UseCacheAdapter();
    }
}