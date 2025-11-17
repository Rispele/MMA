using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Clients.Implementations;
using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Queries.Factories;
using Rooms.Domain.Services;
using Rooms.Infrastructure.Factories;
using Rooms.Infrastructure.ObjectStorageService;
using Rooms.Infrastructure.QueryHandlers.Rooms;

namespace Rooms.Infrastructure.ServicesConfiguration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForRoomsInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<FilterRoomsQueryHandler>();
            })
            .AddScoped<IObjectStorageService, MinioObjectStorageService>()
            .AddScoped<IOperatorDepartmentClient, OperatorDepartmentClient>()
            .AddScoped<IUnitOfWorkFactory, DbContextUnitOfWorkFactory>();
    }
}