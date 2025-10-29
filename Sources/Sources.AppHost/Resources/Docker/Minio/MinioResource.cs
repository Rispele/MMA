namespace Sources.AppHost.Resources.Docker.Minio;

public class MinioResource(string name) : ContainerResource(name), IResourceWithServiceDiscovery;