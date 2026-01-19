using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Minio;

namespace Sources.ServiceDefaults;

public static class MinioConfigurationExtensions
{
    public static IHostApplicationBuilder AddMinio(this IHostApplicationBuilder builder)
    {
        var accessKey = builder.Configuration.GetValue<string>("MINIO_ACCESS_KEY");
        var secretKey = builder.Configuration.GetValue<string>("MINIO_SECRET_KEY");
        var endpoint = builder.Configuration.GetValue<string>("services__minio__http__0") ?? "localhost:10000";

        builder.Services
            .AddMinio(configureClient => configureClient
                .WithEndpoint(endpoint)
                .WithSSL(false)
                .WithCredentials(accessKey, secretKey));

        return builder;
    }
}