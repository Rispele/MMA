using SkbKontur.NUnit.Middlewares;

namespace IntegrationTestInfrastructure.ContainerBasedTests;

public class ContainerTestBase : SimpleTestBase
{
    protected override void Configure(ISetupBuilder fixture, ISetupBuilder test)
    {
        fixture.UseSetup<InjectionSetup>();
    }
}