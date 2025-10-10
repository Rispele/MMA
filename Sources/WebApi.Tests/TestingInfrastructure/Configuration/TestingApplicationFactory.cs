using Aspire.Hosting;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi.Tests.TestingInfrastructure.Configuration;

public class TestingApplicationFactory() : DistributedApplicationFactory(typeof(Projects.Sources_AppHost))
{
    public DistributedApplication Application { get; private set; } = null!;

    protected override void OnBuilderCreating(DistributedApplicationOptions applicationOptions, HostApplicationBuilderSettings hostOptions)
    {
        hostOptions.Configuration ??= new ConfigurationManager();
        hostOptions.Configuration["environment"] = "Development";

        applicationOptions.AllowUnsecuredTransport = true;
        applicationOptions.DisableDashboard = false;
    }

    protected override void OnBuilding(DistributedApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Services.AddLogging(logging => logging
            .AddConsole()
            .AddFilter("Default", LogLevel.Information)
            .AddFilter("Microsoft.AspNetCore", LogLevel.Warning)
            .AddFilter("Aspire.Hosting.Dcp", LogLevel.Warning));

        base.OnBuilding(applicationBuilder);
    }

    protected override void OnBuilt(DistributedApplication application)
    {
        Application = application;

        base.OnBuilt(application);
    }
}