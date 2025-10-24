using Rooms.Persistence;
using Rooms.Persistence.Configuration;
using Sources.ServiceDefaults;

namespace WebApi.Startup;

public static class DbContextConfigurationExtensions
{
    public static IHostApplicationBuilder AddRoomsDbContext(this IHostApplicationBuilder builder)
    {
        return builder.ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
            connectionName: KnownResourceNames.MmrDb,
            NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlRoomsDbContextOptions);
    }
}