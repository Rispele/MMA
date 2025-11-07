using SkbKontur.NUnit.Middlewares;

namespace Rooms.Tests.IntegrationTests;

[SetUpFixture]
public class CoreIntegrationTestsContext : SimpleSuiteBase
{
    protected override void Configure(ISetupBuilder suite)
    {
        suite.UseSetup<CoreIntegrationsTestsSetup>();
    }
}