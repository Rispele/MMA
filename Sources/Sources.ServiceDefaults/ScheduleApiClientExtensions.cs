using Commons.ExternalClients.Booking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minio;

namespace Sources.ServiceDefaults;

public static class ScheduleApiClientExtensions
{
    public static IHostApplicationBuilder AddScheduleApiClientSettings(this IHostApplicationBuilder builder)
    {
        var username = builder.Configuration.GetValue<string>("SCHEDULE_API_USERNAME");
        var password = builder.Configuration.GetValue<string>("SCHEDULE_API_PASSWORD");

        var settings = new BookingClientSettings(
            "http://sked-tst.dit.urfu.ru:8100/",
            username ?? throw new InvalidOperationException("Username is null"),
            password ??  throw new InvalidOperationException("Password is null"));

        builder.Services.AddSingleton(settings);
        
        return builder;
    }
}