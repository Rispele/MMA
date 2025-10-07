using Sources.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUserName = builder.AddParameter("PostgresUserName", secret: true);
var postgresPassword = builder.AddParameter("PostgresUserPassword", secret: true);
var postgresPort = builder.AddParameter("PostgresPort", secret: true);

var port = await postgresPort.Resource.GetValueAsync(CancellationToken.None) ?? throw new InvalidOperationException("Port not specified");

var postgres = builder
    .AddPostgres(KnownResourceNames.PostgresDb, postgresUserName, postgresPassword, port: int.Parse(port))
    .AddDatabase("mmr");

var postgresMigrations = builder
    .AddProject<Projects.Rooms_MigrationService>(KnownResourceNames.RoomsMigrationService)
    .WithReference(postgres)
    .WaitFor(postgres);

builder
    .AddProject<Projects.WebApi>(KnownResourceNames.WebApiService)
    .WithExternalHttpEndpoints()
    .WithReference(postgres)
    .WaitFor(postgresMigrations);

builder.Build().Run();
