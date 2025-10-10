using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sources.AppHost;
using Sources.AppHost.Resources;

var builder = DistributedApplication.CreateBuilder(args);

var serviceCollection = builder.Services;
serviceCollection.AddOptions();

var configuration = new ConfigurationBuilder()
    .AddJsonFile(
        path: "Config/MinioContainerConfig.json",
        optional: false,
        reloadOnChange: true)
    .Build();

serviceCollection.Configure<MinioContainerConfiguration>(configuration.GetSection("MinioContainerConfiguration"));

var postgresUserName = builder.AddParameter("PostgresUserName", secret: true);
var postgresPassword = builder.AddParameter("PostgresUserPassword", secret: true);
var postgresPort = builder.AddParameter("PostgresPort", secret: true);

var port = await postgresPort.Resource.GetValueAsync(CancellationToken.None) ?? throw new InvalidOperationException("Port not specified");

var postgresService = builder
    .AddPostgres(KnownResourceNames.PostgresService, postgresUserName, postgresPassword, port: int.Parse(port))
    .AddDatabase("mmr");

var roomsMigrationService = builder
    .AddProject<Projects.Rooms_MigrationService>(KnownResourceNames.RoomsMigrationService)
    .WithReference(postgresService)
    .WaitFor(postgresService);

builder
    .AddProject<Projects.WebApi>(KnownResourceNames.WebApiService)
    .WithExternalHttpEndpoints()
    .WithReference(postgresService)
    .WaitForCompletion(roomsMigrationService);

var minioConfigSection = configuration.GetSection("MinioContainerConfiguration");
var minioConfig = new MinioContainerConfiguration
{
    Registry = minioConfigSection.GetSection("Registry").Value ?? throw new ArgumentException(),
    Image = minioConfigSection.GetSection("Image").Value ?? throw new ArgumentException(),
    Tag = minioConfigSection.GetSection("Tag").Value ?? throw new ArgumentException(),
};

builder
    .AddMinio(minioConfig, "minio", "admin", "admin123");

builder.Build().Run();
