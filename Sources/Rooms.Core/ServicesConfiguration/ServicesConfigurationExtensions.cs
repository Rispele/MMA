using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Interfaces.Services.Equipments;
using Rooms.Core.Interfaces.Services.OperatorDepartments;
using Rooms.Core.Interfaces.Services.Rooms;
using Rooms.Core.Services.Equipments;
using Rooms.Core.Services.OperatorDepartments;
using Rooms.Core.Services.Rooms;
using Rooms.Core.Services.Spreadsheets;

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