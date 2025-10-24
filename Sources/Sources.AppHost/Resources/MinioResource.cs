using System.Diagnostics.CodeAnalysis;

namespace Sources.AppHost.Resources;

public class MinioResource(string name)
    : ContainerResource(name), IResourceWithServiceDiscovery
{
    internal const string HttpEndpointName = "http";
    internal const string HttpAdminEndpointName = "admin";

    [field: AllowNull]
    [field: MaybeNull]
    public EndpointReference HttpEndpoint => field ??= new EndpointReference(this, HttpEndpointName);

    [field: AllowNull]
    [field: MaybeNull]
    public EndpointReference HttpAdminEndpoint => field ??= new EndpointReference(this, HttpAdminEndpointName);
}