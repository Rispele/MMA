using System.Reflection;
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
using IOperatorDepartmentService = WebApi.Services.Interfaces.IOperatorDepartmentService;
using OperatorDepartmentService = WebApi.Services.Implementations.OperatorDepartmentService;
using IInstituteResponsibleService = WebApi.Services.Interfaces.IInstituteResponsibleService;
using InstituteResponsibleService = WebApi.Services.Implementations.InstituteResponsibleService;
using IRoomScheduleService = WebApi.Services.Interfaces.IRoomScheduleService;
using RoomScheduleService = WebApi.Services.Implementations.RoomScheduleService;
using IBookingRequestService = WebApi.Services.Interfaces.IBookingRequestService;
using BookingRequestService = WebApi.Services.Implementations.BookingRequestService;


namespace WebApi.Startup.ConfigurationExtensions;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForWebApi(this IServiceCollection serviceCollection)
    {
        // serviceCollection.AddOpenApi();

        serviceCollection.AddControllers(options =>
        {
            options.InputFormatters.Insert(index: 0, new StreamInputFormatter());
            options.InputFormatters.Insert(index: 1, JsonPatchInputFormatterProvider.GetJsonPatchInputFormatter());
        });

        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(opt =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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
            .AddScoped<IOperatorDepartmentService, OperatorDepartmentService>()
            .AddScoped<IInstituteResponsibleService, InstituteResponsibleService>()
            .AddScoped<IRoomScheduleService, RoomScheduleService>()
            .AddScoped<IBookingRequestService, BookingRequestService>();

        return serviceCollection;
    }
}