using Microsoft.Extensions.DependencyInjection;
using Rooms.Tests.Helpers.SDK.Equipments.Parts;
using Rooms.Tests.Helpers.SDK.Rooms;

namespace Rooms.Tests.Helpers.SDK;

public static class SdkServicesConfiguration
{
    public static IServiceCollection AddSdkServices(this IServiceCollection services)
    {
        return services
            .AddScoped<RoomsSdk>()
            .AddScoped<EquipmentsSdk>()
            .AddScoped<MmrSdk>();
    }
}