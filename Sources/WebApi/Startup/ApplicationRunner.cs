using Minio;
using Sources.ServiceDefaults;

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

        builder.Services
            .ConfigureServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(b => { b.SwaggerEndpoint("/openapi/v1.json", "My API V1"); });
        }

        app.MapControllers();
        app.UseHttpsRedirection();

        return app;
    }

    public static void Run(WebApplication application)
    {
        application.Run();
    }
}