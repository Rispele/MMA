using Aspire.Hosting.ApplicationModel;
using IntegrationTestInfrastructure.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;
using Rooms.Tests.Helpers.SDK;
using SkbKontur.NUnit.Middlewares;
using Sources.AppHost.Resources;
using WebApi.Startup.ConfigurationExtensions;

namespace WebApi.Tests.IntegrationTests;

[UsedImplicitly]
public class WebApiTestsSetup : ISetup
{
    public async Task SetUpAsync(ITest test)
    {
        var testingApplicationFactory = await BuildApplication();
        var container = await BuildServiceProvider(testingApplicationFactory);

        test.Properties.Set(container);
    }

    public Task TearDownAsync(ITest test)
    {
        var container = test.GetFromThisOrParentContext<IServiceProvider>();

        if (container is IDisposable disposable)
        {
            disposable.Dispose();
        }

        return Task.CompletedTask;
    }

    private static async Task<TestingApplicationFactory> BuildApplication()
    {
        var testingApplicationFactory = new TestingApplicationFactory(testingProfile: "Testing.WebApi");
        await testingApplicationFactory.StartAsync();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        await testingApplicationFactory.Application.ResourceNotifications.WaitForResourceAsync(KnownResources.RoomsMigrationService.Name, KnownResourceStates.Finished, cts.Token);
        await testingApplicationFactory.Application.ResourceNotifications.WaitForResourceHealthyAsync(KnownResources.WebApiService.Name, cts.Token);

        return testingApplicationFactory;
    }

    private static async Task<IServiceProvider> BuildServiceProvider(TestingApplicationFactory testingApplicationFactory)
    {
        var roomsDbContextConnectionString = await testingApplicationFactory
            .GetConnectionString(KnownResources.MmrDb.Name) ?? throw new InvalidOperationException("Database connection string is not set");

        return new TestingContainerFactory()
            .ConfigureRoomsDbContext(roomsDbContextConnectionString)
            .ConfigureServices(t => t
                .ConfigureServicesForWebApi()
                .AddScoped<RoomsSdk>())
            .BuildServiceProvider();
    }
}