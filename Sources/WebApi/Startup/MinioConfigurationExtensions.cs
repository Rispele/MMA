using Minio;
using Sources.ServiceDefaults;

namespace WebApi.Startup;

public static class MinioConfigurationExtensions
{
    public static IHostApplicationBuilder AddMinio(this IHostApplicationBuilder builder)
    {
        var accessKey = builder.Configuration.GetValue<string>("MINIO_ACCESS_KEY");
        var secretKey = builder.Configuration.GetValue<string>("MINIO_SECRET_KEY");

        builder.Services
            .AddMinio(configureClient => configureClient
                // .WithHttpClient(new HttpClient { BaseAddress = new Uri($"https+http://{KnownResourceNames.Minio}") })
                .WithEndpoint("localhost:9000")
                .WithSSL(false)
                // .WithHttpClient(new HttpClient { BaseAddress = new Uri($"https://localhost:9000") })
                .WithCredentials(accessKey, secretKey));

        return builder;
    }
}