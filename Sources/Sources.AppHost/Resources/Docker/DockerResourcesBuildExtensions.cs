using Microsoft.Extensions.Configuration;
using Sources.AppHost.Resources.Docker.Minio;
using Sources.AppHost.Resources.Docker.TestDoubleLkUserApi;
using Sources.AppHost.Resources.Specifications;

namespace Sources.AppHost.Resources.Docker;

public static class DockerResourcesBuildExtensions
{
    #region Postgres

    public static IResourceBuilder<PostgresDatabaseResource> AddPostgresResource(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        ResourceSpecification postgresSpecification,
        ResourceSpecification dbSpecification)
    {
        var postgresUserName = distributedApplicationBuilder.AddParameter(name: "PostgresUserName", secret: true);
        var postgresPassword = distributedApplicationBuilder.AddParameter(name: "PostgresUserPassword", secret: true);

        var postgresService1 = distributedApplicationBuilder
            .AddPostgres(
                postgresSpecification.Name,
                postgresUserName,
                postgresPassword,
                postgresSpecification.GetHttpEndpoint().TargetPort)
            .AddDatabase(dbSpecification.Name);
        return postgresService1;
    }

    #endregion

    #region Minio

    public static MinioResourceParameters AddMinio(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        ResourceSpecification specification,
        IConfigurationRoot configurationRoot)
    {
        var user = distributedApplicationBuilder.AddParameter(name: "MinioRootUser", secret: true);
        var password = distributedApplicationBuilder.AddParameter(name: "MinioRootUserPassword", secret: true);
        var config = configurationRoot
                         .GetSection("MinioContainerConfiguration")
                         .Get<MinioContainerConfiguration>()
                     ?? throw new InvalidOperationException("Minio container configuration not found");

        var resource = distributedApplicationBuilder.AddMinio(config, specification, user, password);
        return new MinioResourceParameters(user, password, resource);
    }

    private static IResourceBuilder<MinioResource> AddMinio(
        this IDistributedApplicationBuilder builder,
        MinioContainerConfiguration config,
        ResourceSpecification specification,
        IResourceBuilder<ParameterResource> rootUser,
        IResourceBuilder<ParameterResource> rootPassword,
        int minioPort = 9000,
        int minioAdminPort = 9001)
    {
        var minioResource = new MinioResource(specification.Name);

        return builder.AddResource(minioResource)
            .WithImage(config.Image)
            .WithImageRegistry(config.Registry)
            .WithImageTag(config.Tag)
            .WithEnvironment(name: "MINIO_ADDRESS", value: ":9000")
            .WithEnvironment(name: "MINIO_CONSOLE_ADDRESS", value: ":9001")
            .WithEnvironment(name: "MINIO_PROMETHEUS_AUTH_TYPE", value: "public")
            .WithHttpEndpoint(
                name: KnownEndpoints.Http,
                port: minioPort,
                targetPort: specification.GetHttpEndpoint().TargetPort)
            .WithHttpEndpoint(
                name: KnownEndpoints.Admin,
                port: minioAdminPort,
                targetPort: specification.GetAdminEndpoint().TargetPort)
            .WithEnvironment(name: "MINIO_ROOT_USER", rootUser)
            .WithEnvironment(name: "MINIO_ROOT_PASSWORD", rootPassword)
            .WithArgs("server", config.Storage);
    }

    #endregion

    #region TestDoubleLkUserApi

    public static TestDoubleLkUserApiResourceParameters AddTestDoubleLkUserApi(
        this IDistributedApplicationBuilder distributedApplicationBuilder,
        ResourceSpecification specification,
        IConfigurationRoot configurationRoot)
    {
        var user = distributedApplicationBuilder.AddParameter(name: "TestDoubleLkUserApiUser", secret: true);
        var password = distributedApplicationBuilder.AddParameter(name: "TestDoubleLkUserApiUserPassword", secret: true);
        var config = configurationRoot
                         .GetSection("TestDoubleLkUserApiConfig")
                         .Get<TestDoubleLkUserApiContainerConfiguration>()
                     ?? throw new InvalidOperationException("Test Double Lk-User-Api container configuration not found");

        var resource = distributedApplicationBuilder.AddTestDoubleLkUserApi(config, specification, user, password);
        return new TestDoubleLkUserApiResourceParameters(user, password, resource);
    }

    public static IResourceBuilder<TestDoubleLkUserApiResource> AddTestDoubleLkUserApi(
        this IDistributedApplicationBuilder builder,
        TestDoubleLkUserApiContainerConfiguration config,
        ResourceSpecification specification,
        IResourceBuilder<ParameterResource> login,
        IResourceBuilder<ParameterResource> password,
        int port = 3413)
    {
        var resource = new TestDoubleLkUserApiResource(specification.Name);

        return builder.AddResource(resource)
            .WithImage(config.Image)
            .WithImageRegistry(config.Registry)
            .WithImageTag(config.Tag)
            .WithHttpEndpoint(
                name: KnownEndpoints.Http,
                port: port,
                targetPort: specification.GetHttpEndpoint().TargetPort)
            .WithEnvironment(name: "AUTH_BASIC_LOGIN", login)
            .WithEnvironment(name: "AUTH_BASIC_PASSWORD", password);
    }

    #endregion
}