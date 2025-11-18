using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Clients.Implementations;
using Rooms.Core.Clients.Interfaces;
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
            .AddScoped<IInstituteDepartmentClient, InstituteDepartmentClient>()
            .AddScoped<IInstituteResponsibleClient, InstituteResponsibleClient>()
            .AddScoped<IRoomScheduleClient, RoomScheduleClient>()
            .AddScoped<IEventHostClient, EventHostClient>()

            .AddScoped<IUnitOfWorkFactory, DbContextUnitOfWorkFactory<RoomsDbContext>>()

            .AddScoped<IRoomQueriesFactory, RoomQueriesFactory>()
            .AddScoped<IEquipmentQueryFactory, EquipmentQueryFactory>()
            .AddScoped<IEquipmentTypeQueryFactory, EquipmentTypeQueryFactory>()
            .AddScoped<IEquipmentSchemaQueryFactory, EquipmentSchemaQueryFactory>()
            .AddScoped<IOperatorDepartmentQueryFactory, OperatorDepartmentQueryFactory>()
            .AddScoped<IInstituteResponsibleQueryFactory, InstituteResponsibleQueryFactory>()
            .AddScoped<IBookingRequestQueryFactory, BookingRequestQueryFactory>();
    }
}