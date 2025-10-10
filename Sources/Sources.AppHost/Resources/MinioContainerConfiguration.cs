namespace Sources.AppHost.Resources;

public class MinioContainerConfiguration
{
    public string Registry { get; set; } = default!;
    public string Image { get; set; } = default!;
    public string Tag { get; set; } = default!;
}