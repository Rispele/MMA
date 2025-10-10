using Sources.AppHost.Resources;

namespace Sources.AppHost;

public static class ContainerBuildExtensions
{
    public static IResourceBuilder<MinioResource> AddMinio(
        this IDistributedApplicationBuilder builder,
        MinioContainerConfiguration config,
        string name,
        string rootUser,
        string rootPassword,
        int minioPort = 9000,
        int minioAdminPort = 9001)
    {
        var minioResource = new MinioResource(name, rootUser, rootPassword);

        return builder.AddResource(minioResource)
            .WithImage(config.Image)
            .WithImageRegistry(config.Registry)
            .WithImageTag(config.Tag)
            .WithEnvironment("MINIO_ADDRESS", ":9000")
            .WithEnvironment("MINIO_CONSOLE_ADDRESS", ":9001")
            .WithEnvironment("MINIO_PROMETHEUS_AUTH_TYPE", "public")
            .WithHttpEndpoint(name: MinioResource.HttpEndpointName, port: minioPort, targetPort: 9000)
            .WithHttpEndpoint(name: MinioResource.HttpAdminEndpointName, port: minioAdminPort, targetPort: 9001)
            .WithEnvironment("MINIO_ROOT_USER", minioResource.RootUser)
            .WithEnvironment("MINIO_ROOT_PASSWORD", minioResource.RootPassword)
            .WithArgs("server", "/data");
    }
}