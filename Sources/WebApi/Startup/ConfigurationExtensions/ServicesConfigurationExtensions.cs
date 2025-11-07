using Rooms.Core.ServicesConfiguration;
using Rooms.Infrastructure.ServicesConfiguration;
using WebApi.Startup.InputFormatters;
using IRoomService = WebApi.Services.Interfaces.IRoomService;
using RoomService = WebApi.Services.Implementations.RoomService;
using IEquipmentService = WebApi.Services.Interfaces.IEquipmentService;
using EquipmentService = WebApi.Services.Implementations.EquipmentService;
using IEquipmentTypeService = WebApi.Services.Interfaces.IEquipmentTypeService;
using EquipmentTypeService = WebApi.Services.Implementations.EquipmentTypeService;
using IEquipmentSchemaService = WebApi.Services.Interfaces.IEquipmentSchemaService;
using EquipmentSchemaService = WebApi.Services.Implementations.EquipmentSchemaService;


namespace WebApi.Startup.ConfigurationExtensions;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForWebApi(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOpenApi();
        serviceCollection.AddEndpointsApiExplorer();

        serviceCollection.AddControllers(options =>
        {
            options.InputFormatters.Insert(index: 0, new StreamInputFormatter());
            options.InputFormatters.Insert(index: 1, JsonPatchInputFormatterProvider.GetJsonPatchInputFormatter());
        });

        serviceCollection.WithServices();

        return serviceCollection;
    }

    private static IServiceCollection WithServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            // Infrastructure
            .ConfigureServicesForRoomsInfrastructure()

            // Core
            .ConfigureServicesForRoomsCore()

            // WebApi
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IEquipmentService, EquipmentService>()
            .AddScoped<IEquipmentTypeService, EquipmentTypeService>()
            .AddScoped<IEquipmentSchemaService, EquipmentSchemaService>()
            .AddScoped<Services.Interfaces.IOperatorDepartmentService, Services.Implementations.OperatorDepartmentService>();

        return serviceCollection;
    }
}