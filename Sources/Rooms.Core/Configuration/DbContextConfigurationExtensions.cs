using Microsoft.Extensions.Hosting;
using Rooms.Persistence;
using Sources.ServiceDefaults;

namespace Rooms.Core.Configuration;

public static class DbContextConfigurationExtensions
{
    public static IHostApplicationBuilder AddRoomsDbContext(this IHostApplicationBuilder builder)
    {
        return builder.ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
            connectionName: KnownResourceNames.MmrDb,
            NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlRoomsDbContextOptions);
    }
}