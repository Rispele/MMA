using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Clients;
using Rooms.Core.Queries.Factories;
using Rooms.Domain.Services;
using Rooms.Infrastructure.Factories;
using Rooms.Infrastructure.ObjectStorageService;

namespace Rooms.Infrastructure.ServicesConfiguration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForRoomsInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IObjectStorageService, MinioObjectStorageService>()
            .AddScoped<IOperatorDepartmentClient, OperatorDepartmentClient>()
            .AddScoped<IUnitOfWorkFactory, DbContextUnitOfWorkFactory<RoomsDbContext>>()
            .AddScoped<IRoomQueriesFactory, RoomQueriesFactory>()
            .AddScoped<IEquipmentQueryFactory, EquipmentQueryFactory>()
            .AddScoped<IEquipmentTypeQueryFactory, EquipmentTypeQueryFactory>()
            .AddScoped<IEquipmentSchemaQueryFactory, EquipmentSchemaQueryFactory>()
            .AddScoped<IOperatorDepartmentQueryFactory, OperatorDepartmentQueryFactory>();
    }
}