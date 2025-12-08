using SkbKontur.NUnit.Middlewares;

namespace Rooms.Migration.Tests;

[SetUpFixture]
[Parallelizable(ParallelScope.None)]
public class RoomsIntegrationTestsContext : SimpleSuiteBase
{
    protected override void Configure(ISetupBuilder suite)
    {
        suite.UseSetup<RoomsIntegrationsTestsSetup>();
    }
}