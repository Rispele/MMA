using Microsoft.Extensions.Configuration;

namespace Sources.AppHost.Resources.ClientSettings;

public static class ClientSettings
{
    public static ScheduleApiClientSettingsParameters AddScheduleApiClientSettingsParameters(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        IConfigurationRoot configurationRoot)
    {
        var config = configurationRoot
                         .GetSection("ScheduleApiConfig")
                         .Get<ScheduleApiClientSettings>()
                     ?? throw new InvalidOperationException("Minio container configuration not found");

        var username = distributedApplicationBuilder
            .AddParameter(
                name: "ScheduleApiUsername",
                secret: true,
                value: config.Username);
        var password = distributedApplicationBuilder
            .AddParameter(
                name: "ScheduleApiPassword",
                secret: true,
                value: config.Password);

        return new ScheduleApiClientSettingsParameters(username, password);
    }

    public static IResourceBuilder<ProjectResource> AddScheduleApiClientSettings(
        this IResourceBuilder<ProjectResource> resourceBuilder,
        ScheduleApiClientSettingsParameters parameters)
    {
        return resourceBuilder
            .WithEnvironment(name: "SCHEDULE_API_USERNAME", parameters.Username)
            .WithEnvironment(name: "SCHEDULE_API_PASSWORD", parameters.Password);
    }
}