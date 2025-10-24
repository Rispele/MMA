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

var minioRootUser = builder.AddParameter("MinioRootUser", secret: true);
var minioRootUserPassword = builder.AddParameter("MinioRootUserPassword", secret: true);

var minioConfig = configuration
                      .GetSection("MinioContainerConfiguration")
                      .Get<MinioContainerConfiguration>()
                  ?? throw new InvalidOperationException("Minio container configuration not found");

var minio = builder.AddMinio(minioConfig, KnownResourceNames.Minio, minioRootUser, minioRootUserPassword);

var postgresUserName = builder.AddParameter("PostgresUserName", secret: true);
var postgresPassword = builder.AddParameter("PostgresUserPassword", secret: true);
var postgresPort = builder.AddParameter("PostgresPort", secret: true);

var port = await postgresPort.Resource.GetValueAsync(CancellationToken.None) ??
           throw new InvalidOperationException("Port not specified");

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
    .WithEnvironment("MINIO_ACCESS_KEY", minioRootUser)
    .WithEnvironment("MINIO_SECRET_KEY", minioRootUserPassword)
    .WithReference(minio)
    .WithReference(postgresService)
    .WaitForCompletion(roomsMigrationService);


builder.Build().Run();