namespace Sources.AppHost.Resources.Docker.TestDoubleLkUserApi;

public record TestDoubleLkUserApiResourceParameters(
    IResourceBuilder<ParameterResource> Username,
    IResourceBuilder<ParameterResource> Password,
    IResourceBuilder<TestDoubleLkUserApiResource> Name);