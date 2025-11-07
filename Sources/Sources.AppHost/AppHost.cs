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

if (testingProfile is "Testing.Core")
{
    builder.AddMinio(KnownResources.Minio, configuration);
    var postgresResource = builder.AddPostgresResource(KnownResources.PostgresService, KnownResources.MmrDb);
    builder.AddRoomsMigration(KnownResources.RoomsMigrationService, postgresResource);
}
else
{
    var minioResourceParameters = builder.AddMinio(KnownResources.Minio, configuration);
    var postgresResource = builder.AddPostgresResource(KnownResources.PostgresService, KnownResources.MmrDb);
    var roomsMigrationResource = builder.AddRoomsMigration(KnownResources.RoomsMigrationService, postgresResource);
    builder.AddWebApi(KnownResources.WebApiService, minioResourceParameters, postgresResource, roomsMigrationResource);
}

builder.Build().Run();