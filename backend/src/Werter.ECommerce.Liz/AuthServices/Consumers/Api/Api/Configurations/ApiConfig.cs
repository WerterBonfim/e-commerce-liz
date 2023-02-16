namespace Api.Configurations;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<DbContext>(options =>
        //     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // services.AddCors(options =>
        //     options.AddPolicy("Total", builder =>
        //         builder
        //             .AllowAnyOrigin()
        //             .AllowAnyMethod()
        //             .AllowAnyHeader()
        //     ));
    }

    public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseCors("Total");

        // PrepararDb.RodarMigrationInicial(app);
    }
}