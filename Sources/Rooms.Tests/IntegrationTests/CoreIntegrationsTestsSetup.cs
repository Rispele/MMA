using Aspire.Hosting.ApplicationModel;
using IntegrationTestInfrastructure.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;
using Rooms.Core.ServicesConfiguration;
using Rooms.Infrastructure.ServicesConfiguration;
using SkbKontur.NUnit.Middlewares;
using Sources.AppHost.Resources;
using WebApi.Tests.SDK;

namespace Rooms.Tests.IntegrationTests;

[UsedImplicitly]
public class CoreIntegrationsTestsSetup : ISetup
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
        var testingApplicationFactory = new TestingApplicationFactory("Testing.Core");
        await testingApplicationFactory.StartAsync();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        await testingApplicationFactory.Application.ResourceNotifications.WaitForResourceAsync(
            KnownResources.RoomsMigrationService.Name,
            targetState: KnownResourceStates.Finished, cts.Token);

        return testingApplicationFactory;
    }

    private static async Task<IServiceProvider> BuildServiceProvider(TestingApplicationFactory testingApplicationFactory)
    {
        var roomsDbContextConnectionString = await testingApplicationFactory
            .GetConnectionString(KnownResources.MmrDb.Name) ?? throw new InvalidOperationException("Database connection string is not set");

        return new TestingContainerFactory()
            .ConfigureRoomsDbContext(roomsDbContextConnectionString)
            .ConfigureServices(t => t
                .ConfigureServicesForRoomsInfrastructure()
                .ConfigureServicesForRoomsCore()
                .AddScoped<RoomsSdk>())
            .BuildServiceProvider();
    }
}