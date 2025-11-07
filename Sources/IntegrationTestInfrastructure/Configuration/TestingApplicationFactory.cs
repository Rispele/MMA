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

        applicationOptions.AllowUnsecuredTransport = true;
        applicationOptions.DisableDashboard = false;
        
        base.OnBuilderCreating(applicationOptions, hostOptions);
    }

    protected override void OnBuilderCreated(DistributedApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Configuration["testing_profile"] = testingProfile;
        
        base.OnBuilderCreated(applicationBuilder);
    }

    protected override void OnBuilding(DistributedApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Services.AddLogging(logging => logging
            .ClearProviders()
            .AddProvider(new TestingLoggerProvider())
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