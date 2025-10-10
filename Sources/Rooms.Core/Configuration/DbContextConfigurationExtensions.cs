using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;
using Rooms.Domain.Persistence;
using Sources.ServiceDefaults;

namespace Rooms.Core.Configuration;

public static class DbContextConfigurationExtensions
{
    public static void AddRoomsDbContext(this IHostApplicationBuilder builder)
    {
        builder.ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
            connectionName: KnownResourceNames.MmrDb,
            ConfigureNpgsqlRoomsDbContextOptions);
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
}