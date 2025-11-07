using Rooms.Infrastructure;
using Rooms.Infrastructure.Configuration;
using Sources.ServiceDefaults;

namespace WebApi.Startup.ConfigurationExtensions;

public static class DbContextConfigurationExtensions
{
    public static IHostApplicationBuilder AddRoomsDbContext(this IHostApplicationBuilder builder)
    {
        return builder.ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
            KnownResourceNames.MmrDb,
            NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlRoomsDbContextOptions);
    }
}