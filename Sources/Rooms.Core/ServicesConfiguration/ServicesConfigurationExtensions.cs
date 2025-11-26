using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Services.BookingRequests;
using Rooms.Core.Services.BookingRequests.Interfaces;
using Rooms.Core.Services.Equipments;
using Rooms.Core.Services.Equipments.Interfaces;
using Rooms.Core.Services.InstituteResponsibles;
using Rooms.Core.Services.InstituteResponsibles.Interfaces;
using Rooms.Core.Services.OperatorDepartments;
using Rooms.Core.Services.OperatorDepartments.Interfaces;
using Rooms.Core.Services.Rooms;
using Rooms.Core.Services.Rooms.Interfaces;
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
            .AddScoped<IInstituteResponsibleService, InstituteResponsibleService>()
            .AddScoped<IRoomScheduleService, RoomScheduleService>()
            .AddScoped<IBookingRequestService, BookingRequestService>()
            .AddScoped<SpreadsheetService>();
    }
}