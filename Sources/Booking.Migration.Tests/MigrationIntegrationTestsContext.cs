using SkbKontur.NUnit.Middlewares;

namespace Booking.Migration.Tests;

[SetUpFixture]
[Parallelizable(ParallelScope.None)]
public class MigrationIntegrationTestsContext : SimpleSuiteBase
{
    protected override void Configure(ISetupBuilder suite)
    {
        suite.UseSetup<MigrationIntegrationsTestsSetup>();
    }
}