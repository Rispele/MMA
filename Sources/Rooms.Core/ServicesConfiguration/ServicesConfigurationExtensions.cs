using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Services.Implementations;
using Rooms.Core.Services.Interfaces;
using Rooms.Core.Spreadsheets;

namespace Rooms.Core.ServicesConfiguration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForRoomsCore(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IEquipmentService, EquipmentService>()
            .AddScoped<IEquipmentTypeService, EquipmentTypeService>()
            .AddScoped<IEquipmentSchemaService, EquipmentSchemaService>()
            .AddScoped<IOperatorDepartmentService, OperatorDepartmentService>()
            .AddScoped<SpreadsheetService>();
    }
}