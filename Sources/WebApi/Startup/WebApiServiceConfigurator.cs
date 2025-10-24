using Minio;
using Rooms.Core.Configuration;
using Rooms.Core.Services.Implementations;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Queries.Factories;
using Rooms.Domain.Services;
using Rooms.Persistence;
using Rooms.Persistence.Factories;
using Rooms.Persistence.Services;
using ICoreRoomService = Rooms.Core.Services.Interfaces.IRoomService;
using CoreRoomService = Rooms.Core.Services.Implementations.RoomService;
using ICoreEquipmentService = Rooms.Core.Services.Interfaces.IEquipmentService;
using CoreEquipmentService = Rooms.Core.Services.Implementations.EquipmentService;

using FileService = WebApi.Services.Implementations.FileService;
using IFileService = WebApi.Services.Interfaces.IFileService;
using IRoomService = WebApi.Services.Interfaces.IRoomService;
using RoomService = WebApi.Services.Implementations.RoomService;
using IEquipmentService = WebApi.Services.Interfaces.IEquipmentService;
using EquipmentService = WebApi.Services.Implementations.EquipmentService;


namespace WebApi.Startup;

public class WebApiServiceConfigurator
{
    public IServiceCollection ConfigureServices(IServiceCollection serviceCollection)
    {
        //OpenApi
        serviceCollection.AddOpenApi();
        serviceCollection.AddEndpointsApiExplorer();

        serviceCollection.AddControllers(options =>
        {
            options.InputFormatters.Insert(index: 0, JsonPatchInputFormatterProvider.GetJsonPatchInputFormatter());
        });

        WithClients(serviceCollection);
        WithOptions(serviceCollection);
        WithServices(serviceCollection);

        return serviceCollection;
    }

    private IServiceCollection WithClients(IServiceCollection serviceCollection)
    {
        //todo: minio
        return serviceCollection;
    }

    private IServiceCollection WithOptions(IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(
                path: "Config/minioOptions.json",
                optional: false,
                reloadOnChange: true)
            .Build();

        serviceCollection.Configure<MinioOptions>(configuration.GetSection("MinioOptions"));

        return serviceCollection;
    }

    private IServiceCollection WithServices(IServiceCollection serviceCollection)
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
            .AddScoped<IFileService, FileService>()
            .AddScoped<IEquipmentService, EquipmentService>();

        return serviceCollection;
    }
}