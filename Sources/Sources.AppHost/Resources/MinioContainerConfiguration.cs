namespace Sources.AppHost.Resources;

public class MinioContainerConfiguration
{
    public string Registry { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Tag { get; set; } = null!;
}