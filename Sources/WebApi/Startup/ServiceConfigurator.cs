// using Application.Clients.Implementations;
// using Application.Clients.Interfaces;
using WebApi.Services.Implementations;
using WebApi.Services.Interfaces;

namespace WebApi.Startup;

public class ServiceConfigurator
{
    public ServiceConfigurator()
    {
        // ...
    }

    public IServiceCollection ConfigureServices(IServiceCollection serviceCollection)
    {
        //OpenApi
        serviceCollection.AddOpenApi();
        serviceCollection.AddEndpointsApiExplorer();

        serviceCollection.AddControllers();

        WithClients(serviceCollection);
        WithServices(serviceCollection);

        return serviceCollection;
    }

    private IServiceCollection WithClients(IServiceCollection serviceCollection)
    {
        // serviceCollection
            // .AddHttpClient<IRoomsClient, RoomsClient>(client =>
            // {
                // client.BaseAddress = new Uri("https+http://application");
            // });
        // serviceCollection
            // .AddHttpClient<IFileClient, FileClient>(client =>
            // {
                // client.BaseAddress = new Uri("https+http://application");
            // })
            // ;

        return serviceCollection;
    }

    private IServiceCollection WithServices(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IRoomService, RoomService>()
            .AddSingleton<IFileService, FileService>()
            ;

        return serviceCollection;
    }
}