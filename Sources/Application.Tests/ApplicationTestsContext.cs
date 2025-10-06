using Aspire.Hosting;

namespace Application.Tests;

[SetUpFixture]
public class ApplicationTestsContext
{
    public static TestingApplicationFactory TestingApplicationFactory { get; private set; } = null!;

    public static DistributedApplication Application => TestingApplicationFactory.Application;

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