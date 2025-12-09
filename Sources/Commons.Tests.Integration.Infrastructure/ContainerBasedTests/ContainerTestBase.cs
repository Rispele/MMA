using SkbKontur.NUnit.Middlewares;

namespace Commons.Tests.Integration.Infrastructure.ContainerBasedTests;

public class ContainerTestBase : SimpleTestBase
{
    protected override void Configure(ISetupBuilder fixture, ISetupBuilder test)
    {
        fixture.UseSetup<InjectionSetup>();
    }
}