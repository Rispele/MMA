using Microsoft.Extensions.DependencyInjection;
using Rooms.Infrastructure;
using Rooms.Infrastructure.Configuration;
using Sources.ServiceDefaults;
using WebApi.Startup.ConfigurationExtensions;

namespace WebApi.Tests.TestingInfrastructure.Configuration;

public class TestingContainerFactory
{
    private readonly IServiceCollection serviceCollection = new ServiceCollection();

    public TestingContainerFactory ConfigureRoomsDbContext(string roomsDbContextConnectionString)
    {
        serviceCollection.ConfigurePostgresDbContext<RoomsDbContext>(
            roomsDbContextConnectionString,
            npgsqlOptionsAction: builder => builder.ConfigureNpgsqlRoomsDbContextOptions());

        return this;
    }

    public TestingContainerFactory ConfigureWebApiServices()
    {
        serviceCollection.ConfigureServices();

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