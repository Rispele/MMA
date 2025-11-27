using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Domain.Services;
using Rooms.Infrastructure.EFCore;
using Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;
using Rooms.Infrastructure.Services.ObjectStorageService;
using Rooms.Infrastructure.Services.Spreadsheets;

namespace Rooms.Infrastructure.Configuration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForRoomsInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<FilterRoomsQueryHandler>(); })
            .AddScoped<IObjectStorageService, MinioObjectStorageService>()
            .AddScoped<IUnitOfWorkFactory, DbContextUnitOfWorkFactory>()
            .AddScoped<ISpreadsheetExporter, ExcelExporter>();
    }
}