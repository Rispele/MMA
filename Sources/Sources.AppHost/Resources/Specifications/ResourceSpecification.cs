namespace Sources.AppHost.Resources.Specifications;

public record ResourceSpecification(string Name, EndpointSpecification[]? Endpoints = null)
{
    public EndpointSpecification GetEndpoint(string name)
    {
        return Endpoints?.FirstOrDefault(e => e.Name == name)
               ?? throw new InvalidOperationException($"Endpoint {name} not found");
    }
}