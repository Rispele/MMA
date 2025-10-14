using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;
using Rooms.Domain.Persistence;
using Sources.ServiceDefaults;

namespace Rooms.Core.Configuration;

public static class DbContextConfigurationExtensions
{
    public static IHostApplicationBuilder AddRoomsDbContext(this IHostApplicationBuilder builder)
    {
        return builder.ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
            connectionName: KnownResourceNames.MmrDb,
            ConfigureNpgsqlRoomsDbContextOptions);
    }

    public static IHostApplicationBuilder AddEquipmentsDbContext(this IHostApplicationBuilder builder)
    {
        return builder.ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, EquipmentsDbContext>(
            connectionName: KnownResourceNames.MmrDb,
            ConfigureNpgsqlEquipmentsDbContextOptions);
    }

    public static void ConfigureNpgsqlRoomsDbContextOptions(this NpgsqlDbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .MapEnum<RoomStatus>()
            .MapEnum<RoomLayout>()
            .MapEnum<RoomNetType>()
            .MapEnum<RoomType>()
            .ConfigureDataSource(b => b.EnableDynamicJson());
    }

    public static void ConfigureNpgsqlEquipmentsDbContextOptions(this NpgsqlDbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .ConfigureDataSource(b => b.EnableDynamicJson());
    }
}