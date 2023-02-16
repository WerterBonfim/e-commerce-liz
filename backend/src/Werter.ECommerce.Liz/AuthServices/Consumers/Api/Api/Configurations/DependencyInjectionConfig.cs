using System.Reflection;
using Application.Commands;
using Core.LogService;
using Data;
using MediatR;

namespace Api.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogManager(configuration);

        // Mediator
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(UsuarioCommandHandler).Assembly));
        
        
        services.UseInMemoryDb();
    }
}