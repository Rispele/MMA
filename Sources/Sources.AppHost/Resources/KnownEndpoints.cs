using Sources.AppHost.Resources.Specifications;

namespace Sources.AppHost.Resources;

public static class KnownEndpoints
{
    public const string Http = "http";
    public const string Https = "https";
    public const string Admin = "admin";

    public static EndpointSpecification GetHttpEndpoint(this ResourceSpecification specification)
    {
        return specification.GetEndpoint(Http);
    }
    
    public static EndpointSpecification GetHttpsEndpoint(this ResourceSpecification specification)
    {
        return specification.GetEndpoint(Https);
    }
    
    public static EndpointSpecification GetAdminEndpoint(this ResourceSpecification specification)
    {
        return specification.GetEndpoint(Admin);
    }
}