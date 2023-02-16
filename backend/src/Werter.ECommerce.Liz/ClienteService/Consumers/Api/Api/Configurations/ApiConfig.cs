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
        app.UseAuthorization();
        app.UseRouting();
        app.UseCors("Total");

        // app.UseAuthConfiguration(env);


        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        // PrepararDb.RodarMigrationInicial(app);
    }
}