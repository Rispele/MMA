using Domain.Models.Room.Fix;
using Domain.Models.Room.Parameters;
using Domain.Persistence;
using Sources.ServiceDefaults;

namespace Application.Configuration;

public static class DbContextConfigurationExtensions
{
    public static void AddDomainContext(this IHostApplicationBuilder builder)
    {
        builder
            .AddPostgresDbContext<IHostApplicationBuilder, DomainDbContext>(
                connectionName: "mmr",
                options => options
                    .MapEnum<RoomStatus>()
                    .MapEnum<RoomLayout>()
                    .MapEnum<RoomNetType>()
                    .MapEnum<RoomType>());
    }
}