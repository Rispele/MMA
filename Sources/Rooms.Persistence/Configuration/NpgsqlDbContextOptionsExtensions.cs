using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Persistence;

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