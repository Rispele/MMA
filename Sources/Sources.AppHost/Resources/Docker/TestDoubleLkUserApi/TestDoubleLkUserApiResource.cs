namespace Sources.AppHost.Resources.Docker.TestDoubleLkUserApi;

public class TestDoubleLkUserApiResource(string name) : ContainerResource(name), IResourceWithServiceDiscovery;