using Aspire.Hosting.ApplicationModel;
using IntegrationTestInfrastructure.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Interfaces;
using Rooms.Infrastructure.Configuration;
using SkbKontur.NUnit.Middlewares;
using Sources.AppHost.Resources;

namespace Rooms.Migration.Tests;

[UsedImplicitly]
public class RoomsIntegrationsTestsSetup : ISetup
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
        var testingApplicationFactory = new TestingApplicationFactory("Testing.Migration");
        await testingApplicationFactory.StartAsync();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        await testingApplicationFactory.Application.ResourceNotifications.WaitForResourceAsync(
            KnownResources.PostgresService.Name,
            targetState: KnownResourceStates.Running,
            cts.Token);

        return testingApplicationFactory;
    }

    private static async Task<IServiceProvider> BuildServiceProvider(TestingApplicationFactory testingApplicationFactory)
    {
        var roomsDbContextConnectionString = await testingApplicationFactory
            .GetConnectionString(KnownResources.MmrDb.Name) ?? throw new InvalidOperationException("Database connection string is not set");

        return new TestingContainerFactory()
            .ConfigureServices(t => t
                .ConfigureRoomsDbContextForTests(roomsDbContextConnectionString)
                .AddLogging(builder => builder.AddConsole()))
            .BuildServiceProvider();
    }
}