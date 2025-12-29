namespace Sources.AppHost.Resources.ClientSettings;

public record ScheduleApiClientSettingsParameters(
    IResourceBuilder<ParameterResource> Username,
    IResourceBuilder<ParameterResource> Password);