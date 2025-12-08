using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sources.AppHost.Resources;
using Sources.AppHost.Resources.Docker;
using Sources.AppHost.Resources.Project;

var builder = DistributedApplication.CreateBuilder(args);

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
        builder.AddRoomsMigration(KnownResources.RoomsMigrationService, postgresResource);
        builder.AddBookingsMigration(KnownResources.BookingsMigrationService, postgresResource);

        builder.AddMinio(KnownResources.Minio, configuration);
        break;
    }
    case "Testing.WebApi":
    case null:
    {
        var roomsMigrationResource = builder.AddRoomsMigration(KnownResources.RoomsMigrationService, postgresResource);
        var bookingMigrationResource = builder.AddBookingsMigration(KnownResources.BookingsMigrationService, postgresResource);

        var minioResourceParameters = builder.AddMinio(KnownResources.Minio, configuration);

        builder.AddWebApi(
            KnownResources.WebApiService,
            minioResourceParameters,
            postgresResource,
            roomsMigrationResource,
            bookingMigrationResource);
        break;
    }
    default:
    {
        throw new ArgumentException($"Unknown testing profile: {testingProfile}");
    }
}

builder.Build().Run();