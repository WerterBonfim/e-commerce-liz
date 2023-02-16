using Microsoft.OpenApi.Models;

namespace Api.Configurations;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API de Clientes",
                Description = "Api que lida com o gerenciamento de clientes",
                Contact = new OpenApiContact { Name = "Werter Bonfim", Email = "werter@hotmail.com.br" },
                License = new OpenApiLicense
                    { Name = "AGPL-3.0", Url = new Uri("https://opensource.org/licenses/AGPL-3.0") }
            });


            // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            // {
            //     Description = "Insira o token JWT desta maneira: Bearer {seu token}",
            //     Name = "Authorization",
            //     Scheme = "Bearer",
            //     BearerFormat = "JWT",
            //     In = ParameterLocation.Header,
            //     Type = SecuritySchemeType.ApiKey
            // });
            //
            // c.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = "Bearer"
            //             }
            //         },
            //         new string[] { }
            //     }
            // });
        });
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
    }
}