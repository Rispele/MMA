using Microsoft.Extensions.DependencyInjection;
using Rooms.Infrastructure.Configuration;
using Rooms.Infrastructure.EFCore;
using Sources.ServiceDefaults;

namespace IntegrationTestInfrastructure.Configuration;

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