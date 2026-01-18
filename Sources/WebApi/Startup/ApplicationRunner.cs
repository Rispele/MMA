using Booking.Infrastructure.Configuration;
using Rooms.Infrastructure.Configuration;
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
            .AddBookingDbContext()
            .AddScheduleApiClientSettings()
            .AddMinio();

        builder
            .Services
            .ConfigureServicesForWebApi(builder.Environment.IsDevelopment())
            .AddOpenApi();

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
            // app.MapOpenApi();
            // app.UseSwaggerUI(b => { b.SwaggerEndpoint(url: "/openapi/v1.json", name: "My API V1"); });
            app.UseSwagger();
            app.UseSwaggerUI(b => { b.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My API V1"); });
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.MapControllers();

        return app;
    }
}