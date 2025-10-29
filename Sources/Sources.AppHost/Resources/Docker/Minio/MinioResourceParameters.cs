namespace Sources.AppHost.Resources.Docker.Minio;

public record MinioResourceParameters(
    IResourceBuilder<ParameterResource> Username,
    IResourceBuilder<ParameterResource> Password,
    IResourceBuilder<MinioResource> Name);