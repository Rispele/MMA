using Microsoft.Extensions.DependencyInjection;

namespace Commons.Tests.Integration.Infrastructure.Configuration;

public class TestingContainerFactory
{
    private readonly IServiceCollection serviceCollection = new ServiceCollection();

    public TestingContainerFactory ConfigureServices(Action<IServiceCollection> configure)
    {
        configure(serviceCollection);

        return this;
    }

    public IServiceProvider BuildServiceProvider()
    {
        return serviceCollection.BuildServiceProvider();
    }
}