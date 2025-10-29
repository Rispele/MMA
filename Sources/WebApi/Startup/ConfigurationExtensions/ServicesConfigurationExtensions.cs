using Mapster;
using MapsterMapper;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Implementations;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Services;
using Rooms.Persistence;
using Rooms.Persistence.Factories;
using Rooms.Persistence.ObjectStorageService;
using WebApi.Startup.InputFormatters;
using ICoreRoomService = Rooms.Core.Services.Interfaces.IRoomService;
using CoreRoomService = Rooms.Core.Services.Implementations.RoomService;
using ICoreEquipmentService = Rooms.Core.Services.Interfaces.IEquipmentService;
using CoreEquipmentService = Rooms.Core.Services.Implementations.EquipmentService;
using ICoreEquipmentTypeService = Rooms.Core.Services.Interfaces.IEquipmentTypeService;
using CoreEquipmentTypeService = Rooms.Core.Services.Implementations.EquipmentTypeService;
using ICoreEquipmentSchemaService = Rooms.Core.Services.Interfaces.IEquipmentSchemaService;
using CoreEquipmentSchemaService = Rooms.Core.Services.Implementations.EquipmentSchemaService;
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
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
    {
        //OpenApi
        serviceCollection.AddOpenApi();
        serviceCollection.AddEndpointsApiExplorer();

        serviceCollection.AddControllers(options =>
        {
            options.InputFormatters.Insert(index: 0, new StreamInputFormatter());
            options.InputFormatters.Insert(index: 1, JsonPatchInputFormatterProvider.GetJsonPatchInputFormatter());
        });

        serviceCollection
            .WithMapster()
            .WithServices();

        return serviceCollection;
    }

    private static IServiceCollection WithMapster(this IServiceCollection serviceCollection)
    {
        var config = new TypeAdapterConfig().ConfigureMapster();

        serviceCollection.AddSingleton(config);
        serviceCollection.AddScoped<IMapper, ServiceMapper>();

        return serviceCollection;
    }

    private static IServiceCollection WithServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            // Infrastructure
            .AddScoped<IObjectStorageService, MinioObjectStorageService>()
            .AddScoped<IUnitOfWorkFactory, DbContextUnitOfWorkFactory<RoomsDbContext>>()
            .AddScoped<IRoomQueriesFactory, RoomQueriesFactory>()
            .AddScoped<IEquipmentQueryFactory, EquipmentQueryFactory>()
            .AddScoped<IEquipmentTypeQueryFactory, EquipmentTypeQueryFactory>()
            .AddScoped<IEquipmentSchemaQueryFactory, EquipmentSchemaQueryFactory>()

            // Core
            .AddScoped<IRoomAttachmentsService, RoomAttachmentsService>()
            .AddScoped<ICoreRoomService, CoreRoomService>()
            .AddScoped<ICoreEquipmentService, CoreEquipmentService>()
            .AddScoped<ICoreEquipmentTypeService, CoreEquipmentTypeService>()
            .AddScoped<ICoreEquipmentSchemaService, CoreEquipmentSchemaService>()

            // WebApi
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IEquipmentService, EquipmentService>()
            .AddScoped<IEquipmentTypeService, EquipmentTypeService>()
            .AddScoped<IEquipmentSchemaService, EquipmentSchemaService>()
            ;

        return serviceCollection;
    }
}