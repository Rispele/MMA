using Rooms.Core.Implementations.Services.Files;
using WebApi.Services.Implementations;
using WebApi.Services.Interfaces;

using ICoreRoomService = Rooms.Core.Implementations.Services.Rooms.IRoomService;
using CoreRoomService = Rooms.Core.Implementations.Services.Rooms.RoomService;
using ICoreFileService = Rooms.Core.Implementations.Services.Files.IFileService;
using CoreFileService = Rooms.Core.Implementations.Services.Files.FileService;

using FileService = WebApi.Services.Implementations.FileService;
using IFileService = WebApi.Services.Interfaces.IFileService;


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
            .AddScoped<IMinioStorageService, MinioStorageService>()
            .AddScoped<ICoreRoomService, CoreRoomService>()
            .AddScoped<ICoreFileService, CoreFileService>()
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IFileService, FileService>()
            .AddScoped<IEquipmentService, EquipmentService>()
            ;

        return serviceCollection;
    }
}