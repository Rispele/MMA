using Commons.Domain.Queries.Factories;
using Microsoft.Extensions.DependencyInjection;
using Rooms.Core;
using Rooms.Core.Interfaces.Services.Equipments;
using Rooms.Core.Interfaces.Services.OperatorDepartments;
using Rooms.Core.Interfaces.Services.Rooms;
using Rooms.Core.Services.Equipments;
using Rooms.Core.Services.OperatorDepartments;
using Rooms.Core.Services.Rooms;
using Rooms.Core.Services.Spreadsheets;
using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Domain.Services;
using Rooms.Infrastructure.EFCore;
using Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;
using Rooms.Infrastructure.Services.ObjectStorageService;
using Rooms.Infrastructure.Services.Spreadsheets;

namespace Rooms.Infrastructure.Configuration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForRooms(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddKeyedScoped<IUnitOfWorkFactory, RoomsDbContextUnitOfWorkFactory>(KnownScopes.Rooms)
          
            .AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<FilterRoomsQueryHandler>(); })
            .AddScoped<IObjectStorageService, MinioObjectStorageService>()
            .AddScoped<ISpreadsheetExporter, ExcelExporter>()
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IEquipmentService, EquipmentService>()
            .AddScoped<IEquipmentTypeService, EquipmentTypeService>()
            .AddScoped<IEquipmentSchemaService, EquipmentSchemaService>()
            .AddScoped<IOperatorDepartmentService, OperatorDepartmentService>()
            .AddScoped<SpreadsheetService>();
    }
}