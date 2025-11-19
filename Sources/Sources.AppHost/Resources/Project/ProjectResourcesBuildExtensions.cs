using Projects;
using Sources.AppHost.Resources.Docker.Minio;
using Sources.AppHost.Resources.Specifications;

namespace Sources.AppHost.Resources.Project;

public static class ProjectResourcesBuildExtensions
{
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

    public static IResourceBuilder<ProjectResource> AddWebApi(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        ResourceSpecification resourceSpecification,
        MinioResourceParameters minioResourceParameters,
        // TestDoubleLkUserApiResourceParameters testDoubleLkUserApiParameters,
        IResourceBuilder<PostgresDatabaseResource> postgresResource2,
        IResourceBuilder<ProjectResource> roomsMigrationService2)
    {
        return distributedApplicationBuilder
            .AddProject<WebApi>(resourceSpecification.Name)
            .WithExternalHttpEndpoints()
            // .WithHttpEndpoint(httpEndpoint.TargetPort)
            // .WithHttpsEndpoint(httpsEndpoint.TargetPort)

            #region Minio

            .WithEnvironment(name: "MINIO_ACCESS_KEY", minioResourceParameters.Username)
            .WithEnvironment(name: "MINIO_SECRET_KEY", minioResourceParameters.Password)
            .WithReference(minioResourceParameters.Name)

            #endregion

            // #region TestDoubleLkUserApi
            //
            // .WithEnvironment(name: "TEST_DOUBLE_LK_USER_API_USERNAME", testDoubleLkUserApiParameters.Username)
            // .WithEnvironment(name: "TEST_DOUBLE_LK_USER_API_PASSWORD", testDoubleLkUserApiParameters.Password)
            // .WithReference(testDoubleLkUserApiParameters.Name)
            //
            // #endregion
            .WithReference(postgresResource2)
            .WaitForCompletion(roomsMigrationService2);
    }
}