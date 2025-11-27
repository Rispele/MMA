using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTestInfrastructure.Configuration;

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