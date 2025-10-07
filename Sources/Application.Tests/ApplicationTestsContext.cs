using Application.Clients.Implementations;
using Application.Tests.SDK;
using Sources.AppHost;

namespace Application.Tests;

[SetUpFixture]
public class ApplicationTestsContext
{
    public static TestingApplicationFactory TestingApplicationFactory { get; private set; } = null!;

    public static RoomsClient RoomsClient => new(TestingApplicationFactory.CreateHttpClient(KnownResourceNames.ApplicationService));

    public static RoomsSdk RoomsSdk => new(RoomsClient);

    [OneTimeSetUp]
    public async Task SetUp()
    {
        TestingApplicationFactory = new TestingApplicationFactory();

        await TestingApplicationFactory.StartAsync();

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        await TestingApplicationFactory.Application.ResourceNotifications.WaitForResourceHealthyAsync("application", cts.Token);
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await TestingApplicationFactory.DisposeAsync();
    }
}