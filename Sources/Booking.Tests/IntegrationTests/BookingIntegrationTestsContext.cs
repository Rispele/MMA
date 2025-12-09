using SkbKontur.NUnit.Middlewares;

namespace Booking.Tests.IntegrationTests;

[SetUpFixture]
[Parallelizable(ParallelScope.None)]
public class BookingIntegrationTestsContext : SimpleSuiteBase
{
    protected override void Configure(ISetupBuilder suite)
    {
        suite.UseSetup<BookingIntegrationsTestsSetup>();
    }
}