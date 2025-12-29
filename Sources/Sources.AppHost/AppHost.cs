using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sources.AppHost.Resources;
using Sources.AppHost.Resources.ClientSettings;
using Sources.AppHost.Resources.Docker;
using Sources.AppHost.Resources.Project;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("dev")
    .WithDashboard(dashboard =>
    {
        dashboard.WithHostPort(19199).WithForwardedHeaders(enabled: true);
    });

var serviceCollection = builder.Services;
serviceCollection.AddOptions();

var configuration = new ConfigurationBuilder()
    .AddJsonFile(
        path: "Config/MinioContainerConfig.json",
        optional: false,
        reloadOnChange: true)
    .AddJsonFile(
        path: "Config/TestDoubleLkUserApiConfig.json",
        optional: false,
        reloadOnChange: true)
    .AddJsonFile(
        path: "Config/ScheduleApiConfig.json",
        optional: false,
        reloadOnChange: true)
    .Build();

var testingProfile = builder.Configuration["testing_profile"];

var postgresResource = builder.AddPostgresResource(KnownResources.PostgresService, KnownResources.MmrDb);
switch (testingProfile)
{
    case "Testing.Migration":
    {
        break;
    }
    case "Testing.Core":
    {
        var roomsMigrationResource = builder.AddRoomsMigration(KnownResources.RoomsMigrationService, postgresResource);
        var bookingMigrationResource = builder.AddBookingsMigration(KnownResources.BookingsMigrationService, postgresResource);

        var minioResourceParameters = builder.AddMinio(KnownResources.Minio, configuration);
        builder
            .AddBookingOrchestrator(KnownResources.BookingOrchestrator)
            .ReferenceMinio(minioResourceParameters)
            .WaitForMigrations(postgresResource, roomsMigrationResource, bookingMigrationResource);
        break;
    }
    case "Testing.WebApi":
    case null:
    {
        var roomsMigrationResource = builder.AddRoomsMigration(KnownResources.RoomsMigrationService, postgresResource);
        var bookingMigrationResource = builder.AddBookingsMigration(KnownResources.BookingsMigrationService, postgresResource);

        var minioResourceParameters = builder.AddMinio(KnownResources.Minio, configuration);
        var scheduleApiParameters = builder.AddScheduleApiClientSettingsParameters(configuration);
        builder
            .AddBookingOrchestrator(KnownResources.BookingOrchestrator)
            .AddScheduleApiClientSettings(scheduleApiParameters)
            .ReferenceMinio(minioResourceParameters)
            .WaitForMigrations(postgresResource, roomsMigrationResource, bookingMigrationResource);

        builder
            .AddWebApi(KnownResources.WebApiService)
            .AddScheduleApiClientSettings(scheduleApiParameters)
            .WaitForMigrations(postgresResource, roomsMigrationResource, bookingMigrationResource)
            .ReferenceMinio(minioResourceParameters);

        break;
    }
    default:
    {
        throw new ArgumentException($"Unknown testing profile: {testingProfile}");
    }
}

builder.Build().Run();