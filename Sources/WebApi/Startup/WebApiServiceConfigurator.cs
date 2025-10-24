using Rooms.Core.Services.Implementations;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Queries.Factories;
using Rooms.Domain.Services;
using Rooms.Persistence;
using Rooms.Persistence.Factories;
using Rooms.Persistence.ObjectStorageService;
using ICoreRoomService = Rooms.Core.Services.Interfaces.IRoomService;
using CoreRoomService = Rooms.Core.Services.Implementations.RoomService;
using ICoreEquipmentService = Rooms.Core.Services.Interfaces.IEquipmentService;
using CoreEquipmentService = Rooms.Core.Services.Implementations.EquipmentService;

using IRoomService = WebApi.Services.Interfaces.IRoomService;
using RoomService = WebApi.Services.Implementations.RoomService;
using IEquipmentService = WebApi.Services.Interfaces.IEquipmentService;
using EquipmentService = WebApi.Services.Implementations.EquipmentService;


namespace WebApi.Startup;

public static class WebApiServiceConfigurator
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

        serviceCollection.WithServices();
        
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

            // Core
            .AddScoped<IRoomAttachmentsService, RoomAttachmentsService>()
            .AddScoped<ICoreRoomService, CoreRoomService>()
            .AddScoped<ICoreEquipmentService, CoreEquipmentService>()
            
            // WebApi
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IEquipmentService, EquipmentService>();

        return serviceCollection;
    }
}