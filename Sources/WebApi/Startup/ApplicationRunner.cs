using Minio;
using Rooms.Core.Configuration;
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
            .Services.AddMinio(configureClient => configureClient
                .WithHttpClient(new HttpClient { BaseAddress = new Uri("https+http://minio") })
                .WithCredentials(
                    accessKey: builder.Configuration.GetValue<string>("MINIO_ACCESS_KEY"),
                    secretKey: builder.Configuration.GetValue<string>("MINIO_SECRET_KEY")));

        var serviceConfigurator = new WebApiServiceConfigurator();
        serviceConfigurator.ConfigureServices(builder.Services);

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