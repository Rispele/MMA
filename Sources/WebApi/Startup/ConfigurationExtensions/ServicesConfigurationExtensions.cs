using System.Reflection;
using Booking.Infrastructure.Configuration;
using Rooms.Infrastructure.Configuration;
using WebApi.Services.Implementations;
using WebApi.Services.Interfaces;
using WebApi.Startup.InputFormatters;

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
            .ConfigureServicesForRooms()
            .ConfigureServicesForBooking()

            // WebApi
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IEquipmentService, EquipmentService>()
            .AddScoped<IEquipmentTypeService, EquipmentTypeService>()
            .AddScoped<IEquipmentSchemaService, EquipmentSchemaService>()
            .AddScoped<IOperatorDepartmentService, OperatorDepartmentService>()
            .AddScoped<IInstituteResponsibleService, InstituteCoordinatorService>()
            .AddScoped<IRoomScheduleService, RoomScheduleService>()
            .AddScoped<IBookingRequestService, BookingRequestService>();

        return serviceCollection;
    }
}