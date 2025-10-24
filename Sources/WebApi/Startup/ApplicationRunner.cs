using Sources.ServiceDefaults;
using WebApi.Startup.ConfigurationExtensions;

namespace WebApi.Startup;

public static class ApplicationRunner
{
    public static WebApplication Build(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder
            .AddServiceDefaults()
            .AddRoomsDbContext()
            .AddMinio();

        builder.Services.ConfigureServices();

        return ConfigureApplication(builder);
    }

    public static void Run(WebApplication application)
    {
        application.Run();
    }

    private static WebApplication ConfigureApplication(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(b => { b.SwaggerEndpoint("/openapi/v1.json", "My API V1"); });
        }

        app.MapControllers();
        app.UseHttpsRedirection();

        return app;
    }
}