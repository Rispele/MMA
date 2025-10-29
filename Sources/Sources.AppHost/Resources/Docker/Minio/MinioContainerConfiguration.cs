namespace Sources.AppHost.Resources.Docker.Minio;

public class MinioContainerConfiguration
{
    public string Registry { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public string Storage { get; set; } = null!;
}