using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Infrastructure.Configuration;

public static class NpgsqlDbContextOptionsExtensions
{
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