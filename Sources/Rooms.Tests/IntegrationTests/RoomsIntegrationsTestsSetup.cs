using Aspire.Hosting.ApplicationModel;
using Commons.Domain.Queries.Factories;
using Commons.Tests.Helpers.SDK;
using Commons.Tests.Integration.Infrastructure.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Interfaces;
using Rooms.Infrastructure.Configuration;
using Rooms.Infrastructure.EFCore;
using SkbKontur.NUnit.Middlewares;
using Sources.AppHost.Resources;
using Sources.AppHost.Resources.ClientSettings;
using Sources.ServiceDefaults;

namespace Rooms.Tests.IntegrationTests;

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
        var testingApplicationFactory = new TestingApplicationFactory("Testing.Core");
        await testingApplicationFactory.StartAsync();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        await testingApplicationFactory.Application.ResourceNotifications.WaitForResourceAsync(
            KnownResources.RoomsMigrationService.Name,
            KnownResourceStates.Finished,
            cts.Token);

        return testingApplicationFactory;
    }

    private static async Task<IServiceProvider> BuildServiceProvider(TestingApplicationFactory testingApplicationFactory)
    {
        var roomsDbContextConnectionString = await testingApplicationFactory
            .GetConnectionString(KnownResources.MmrDb.Name) ?? throw new InvalidOperationException("Database connection string is not set");
        var configuration = testingApplicationFactory.Application.Services.GetRequiredService<IConfiguration>();
        var scheduleApiClientSettings = configuration.GetScheduleApiClientSettings();
        
        return new TestingContainerFactory()
            .ConfigureServices(t => t
                .AddHttpClient()
                .AddScheduleApiClientSettingsForTests(scheduleApiClientSettings.Username, scheduleApiClientSettings.Password)
                .AddScoped<IUnitOfWorkFactory, RoomsDbContextUnitOfWorkFactory>()
                .ConfigureRoomsDbContextForTests(roomsDbContextConnectionString)
                .AddLogging(builder => builder.AddConsole())
                .ConfigureServicesForRooms()
                .AddSdkServices())
            .BuildServiceProvider();
    }
}