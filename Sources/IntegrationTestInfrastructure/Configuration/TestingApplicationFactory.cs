using Aspire.Hosting;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Projects;

namespace IntegrationTestInfrastructure.Configuration;

public class TestingApplicationFactory(string testingProfile) : DistributedApplicationFactory(typeof(Sources_AppHost))
{
    public DistributedApplication Application { get; private set; } = null!;

    protected override void OnBuilderCreating(DistributedApplicationOptions applicationOptions, HostApplicationBuilderSettings hostOptions)
    {
        hostOptions.Configuration ??= new ConfigurationManager();
        hostOptions.Configuration["environment"] = "Development";
        hostOptions.Configuration["testing_profile"] = testingProfile;

        applicationOptions.AllowUnsecuredTransport = true;
        applicationOptions.DisableDashboard = false;
    }

    protected override void OnBuilding(DistributedApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Services.AddLogging(logging => logging
            .AddConsole()
            .AddFilter(category: "Default", LogLevel.Information)
            .AddFilter(category: "Microsoft.AspNetCore", LogLevel.Warning)
            .AddFilter(category: "Aspire.Hosting.Dcp", LogLevel.Warning));

        base.OnBuilding(applicationBuilder);
    }

    protected override void OnBuilt(DistributedApplication application)
    {
        Application = application;

        base.OnBuilt(application);
    }
}