namespace Sources.AppHost.Resources;

public class MinioResource(string name, string rootUser, string rootPassword)
    : ContainerResource(name), IResourceWithServiceDiscovery
{
    internal const string HttpEndpointName = "http";
    internal const string HttpAdminEndpointName = "admin";

    public string RootUser { get; } = rootUser;
    public string RootPassword { get; } = rootPassword;

    private EndpointReference? _httpEndpointReference;
    private EndpointReference? _httpAdminEndpointReference;

    public EndpointReference HttpEndpoint =>
        _httpEndpointReference ??= new EndpointReference(this, HttpEndpointName);

    public EndpointReference HttpAdminEndpoint =>
        _httpAdminEndpointReference ??= new EndpointReference(this, HttpAdminEndpointName);
}