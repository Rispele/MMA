using Commons.Tests.Helpers.SDK.Equipments.Parts;
using Commons.Tests.Helpers.SDK.Rooms;
using Microsoft.Extensions.DependencyInjection;

namespace Commons.Tests.Helpers.SDK;

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