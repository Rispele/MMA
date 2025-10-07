using Microsoft.Extensions.Hosting;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;
using Rooms.Domain.Persistence;
using Sources.ServiceDefaults;

namespace Rooms.Core.Configuration;

public static class DbContextConfigurationExtensions
{
    public static void AddRoomsDbContext(this IHostApplicationBuilder builder)
    {
        builder
            .AddPostgresDbContext<IHostApplicationBuilder, RoomsDbContext>(
                connectionName: "mmr",
                options => options
                    .MapEnum<RoomStatus>()
                    .MapEnum<RoomLayout>()
                    .MapEnum<RoomNetType>()
                    .MapEnum<RoomType>()
                    .ConfigureDataSource(b => b.EnableDynamicJson()));
    }
}