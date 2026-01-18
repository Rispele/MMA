using System.Reflection;
using System.Text.Json.Serialization;
using Booking.Infrastructure.Configuration;
using Commons.ExternalClients.Booking;
using Rooms.Infrastructure.Configuration;
using WebApi.Core.Services.Implementations;
using WebApi.Core.Services.Interfaces;
using WebApi.Startup.InputFormatters;

namespace WebApi.Startup.ConfigurationExtensions;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForWebApi(this IServiceCollection serviceCollection, bool isDevelopment)
    {
        // serviceCollection.AddOpenApi();

        serviceCollection
            .AddControllers(options =>
            {
                options.InputFormatters.Insert(index: 1, JsonPatchInputFormatterProvider.GetJsonPatchInputFormatter());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(opt =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        serviceCollection.WithServices(isDevelopment);

        return serviceCollection;
    }

    private static IServiceCollection WithServices(this IServiceCollection serviceCollection, bool isDevelopment)
    {
        serviceCollection
            // Infrastructure
            .AddSingleton<IBookingClient, BookingClient>()
            .ConfigureServicesForRooms()
            .ConfigureServicesForBooking(isDevelopment)

            // WebApi
            .AddScoped<IInternalApiService, InternalApiService>()
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IEquipmentService, EquipmentService>()
            .AddScoped<IEquipmentTypeService, EquipmentTypeService>()
            .AddScoped<IEquipmentSchemaService, EquipmentSchemaService>()
            .AddScoped<IOperatorDepartmentService, OperatorDepartmentService>()
            .AddScoped<IInstituteCoordinatorService, InstituteCoordinatorService>()
            .AddScoped<IRoomScheduleService, RoomScheduleService>()
            .AddScoped<IBookingRequestService, BookingRequestService>();

        return serviceCollection;
    }
}