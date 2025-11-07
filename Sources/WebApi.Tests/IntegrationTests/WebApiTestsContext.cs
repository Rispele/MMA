using SkbKontur.NUnit.Middlewares;

namespace WebApi.Tests.IntegrationTests;

[SetUpFixture]
public class WebApiTestsContext : SimpleSuiteBase
{
    protected override void Configure(ISetupBuilder suite)
    {
        suite.UseSetup<WebApiTestsSetup>();
    }
}