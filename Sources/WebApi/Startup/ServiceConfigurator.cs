using Rooms.Core.Configuration;
using Rooms.Core.Implementations.Services.Files;
using Rooms.Core.Implementations.Services.Rooms;

namespace WebApi.Startup;

public class ServiceConfigurator
{
    public IServiceCollection ConfigureServices(IHostApplicationBuilder applicationBuilder)
    {
        applicationBuilder.AddRoomsDbContext();
        
        var serviceCollection = applicationBuilder.Services;
        
        //OpenApi
        serviceCollection.AddOpenApi();
        serviceCollection.AddEndpointsApiExplorer();

        serviceCollection.AddControllers();
        
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
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IFileService, FileService>();

        return serviceCollection;
    }
}