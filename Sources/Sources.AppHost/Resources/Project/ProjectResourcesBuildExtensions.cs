using Projects;
using Sources.AppHost.Resources.Docker.Minio;
using Sources.AppHost.Resources.Specifications;

namespace Sources.AppHost.Resources.Project;

public static class ProjectResourcesBuildExtensions
{
    public static IResourceBuilder<ProjectResource> WaitForMigrations(
        this IResourceBuilder<ProjectResource> resourceBuilder,
        IResourceBuilder<PostgresDatabaseResource> postgresResource,
        IResourceBuilder<ProjectResource> roomsMigrationResource,
        IResourceBuilder<ProjectResource> bookingMigrationResource)
    {
        return resourceBuilder
            .WithReference(postgresResource)
            .WaitForCompletion(roomsMigrationResource)
            .WaitForCompletion(bookingMigrationResource);
    }

    public static IResourceBuilder<ProjectResource> ReferenceMinio(
        this IResourceBuilder<ProjectResource> resourceBuilder,
        MinioResourceParameters minioResourceParameters)
    {
        return resourceBuilder
            .WithEnvironment(name: "MINIO_ACCESS_KEY", minioResourceParameters.Username)
            .WithEnvironment(name: "MINIO_SECRET_KEY", minioResourceParameters.Password)
            .WithReference(minioResourceParameters.Name);
    }

    public static IResourceBuilder<ProjectResource> AddRoomsMigration(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        ResourceSpecification resourceSpecification,
        IResourceBuilder<PostgresDatabaseResource> postgresResource1)
    {
        return distributedApplicationBuilder
            .AddProject<Rooms_MigrationService>(resourceSpecification.Name)
            .WithReference(postgresResource1)
            .WaitFor(postgresResource1);
    }

    public static IResourceBuilder<ProjectResource> AddBookingsMigration(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        ResourceSpecification resourceSpecification,
        IResourceBuilder<PostgresDatabaseResource> postgresResource1)
    {
        return distributedApplicationBuilder
            .AddProject<Booking_MigrationService>(resourceSpecification.Name)
            .WithReference(postgresResource1)
            .WaitFor(postgresResource1);
    }

    public static IResourceBuilder<ProjectResource> AddBookingOrchestrator(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        ResourceSpecification resourceSpecification)
    {
        return distributedApplicationBuilder.AddProject<Booking_Orchestrator>(resourceSpecification.Name);
    }

    public static IResourceBuilder<ProjectResource> AddWebApi(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        ResourceSpecification resourceSpecification)
    {
        var webApiPort = resourceSpecification.GetHttpEndpoint().TargetPort;
        return distributedApplicationBuilder
            .AddProject<WebApi>(resourceSpecification.Name)
            .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
            // .WithHttpEndpoint(port: webApiPort, targetPort: webApiPort, name: "WebApiPort", isProxied: false)
            // .WithHttpEndpoint(port: 5049, targetPort: 5049, name: "WebApiExternalPort")
            .WithExternalHttpEndpoints();
    }
}