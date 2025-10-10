using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Configuration;
using Rooms.Domain.Persistence;
using Sources.ServiceDefaults;
using WebApi.Startup;

namespace WebApi.Tests.TestingInfrastructure.Configuration;

public class TestingContainerFactory
{
    private readonly IServiceCollection serviceCollection = new ServiceCollection();

    public TestingContainerFactory ConfigureRoomsDbContext(string roomsDbContextConnectionString)
    {
        serviceCollection.ConfigurePostgresDbContext<RoomsDbContext>(
            roomsDbContextConnectionString,
            builder => builder.ConfigureNpgsqlRoomsDbContextOptions());

        return this;
    }

    public TestingContainerFactory ConfigureWebApiServices()
    {
        new WebApiServiceConfigurator().ConfigureServices(serviceCollection);

        return this;
    }

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