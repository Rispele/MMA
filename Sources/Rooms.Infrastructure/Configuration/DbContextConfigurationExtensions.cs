using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rooms.Infrastructure.EFCore;
using Sources.ServiceDefaults;

namespace Rooms.Infrastructure.Configuration;

public static class DbContextConfigurationExtensions
{
    public static IServiceCollection ConfigureRoomsDbContextForTests(this IServiceCollection services, string roomsDbContextConnectionString)
    {
        return services.ConfigurePostgresDbContext<RoomsDbContext>(
            roomsDbContextConnectionString,
            npgsqlOptionsAction: builder => builder.ConfigureNpgsqlRoomsDbContextOptions());
    }

    
    public static IHostApplicationBuilder AddRoomsDbContext(this IHostApplicationBuilder builder)
    {
        return builder.ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
            KnownResourceNames.MmrDb,
            NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlRoomsDbContextOptions);
    }
}