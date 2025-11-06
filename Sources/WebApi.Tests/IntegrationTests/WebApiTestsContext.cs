using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.DependencyInjection;
using Sources.AppHost.Resources;
using WebApi.Tests.SDK;
using WebApi.Tests.TestingInfrastructure.Configuration;

namespace WebApi.Tests.IntegrationTests;

[SetUpFixture]
public class WebApiTestsContext
{
    public static TestingApplicationFactory TestingApplicationFactory { get; private set; } = null!;

    public static IServiceProvider ServiceProvider { get; private set; } = null!;


    [OneTimeSetUp]
    public async Task SetUp()
    {
        await BuildApplication();
        await BuildServiceProvider();
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await TestingApplicationFactory.DisposeAsync();
    }

    private static async Task BuildApplication()
    {
        TestingApplicationFactory = new TestingApplicationFactory();

        await TestingApplicationFactory.StartAsync();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        await TestingApplicationFactory.Application.ResourceNotifications.WaitForResourceAsync(KnownResources.RoomsMigrationService.Name, KnownResourceStates.Finished, cts.Token);
        await TestingApplicationFactory.Application.ResourceNotifications.WaitForResourceHealthyAsync(KnownResources.WebApiService.Name, cts.Token);
    }

    private static async Task BuildServiceProvider()
    {
        var roomsDbContextConnectionString = await TestingApplicationFactory
            .GetConnectionString(KnownResources.MmrDb.Name) ?? throw new InvalidOperationException("Database connection string is not set");

        ServiceProvider = new TestingContainerFactory()
            .ConfigureRoomsDbContext(roomsDbContextConnectionString)
            .ConfigureWebApiServices()
            .ConfigureServices(t => t.AddScoped<RoomsSdk>())
            .BuildServiceProvider();
    }
}