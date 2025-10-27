namespace Sources.AppHost.Resources;

public class MinioResource(string name)
    : ContainerResource(name), IResourceWithServiceDiscovery
{
    internal const string HttpEndpointName = "http";
    internal const string HttpAdminEndpointName = "admin";
}