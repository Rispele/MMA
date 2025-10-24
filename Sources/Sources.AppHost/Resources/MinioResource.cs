using System.Diagnostics.CodeAnalysis;

namespace Sources.AppHost.Resources;

public class MinioResource(string name)
    : ContainerResource(name), IResourceWithServiceDiscovery
{
    internal const string HttpEndpointName = "http";
    internal const string HttpAdminEndpointName = "admin";

    [field: AllowNull, MaybeNull]
    public EndpointReference HttpEndpoint => field ??= new EndpointReference(this, HttpEndpointName);

    [field: AllowNull, MaybeNull]
    public EndpointReference HttpAdminEndpoint => field ??= new EndpointReference(this, HttpAdminEndpointName);
}