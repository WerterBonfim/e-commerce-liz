using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyInjection
{
    public static void UseInMemoryDb(this IServiceCollection services)
    {
        services.AddDbContext<AuthRepositorio>(options => { options.UseInMemoryDatabase("ECommerce-Liz"); });

        services.AddIdentity<UsuarioDaAplicacao, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AuthRepositorio>()
            .AddDefaultTokenProviders();
    }
}