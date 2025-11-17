namespace WebApi.Models.Files;

public class FileExportModel
{
    public required string FileName { get; set; }
    public required Stream Content { get; set; }
    public required string ContentType { get; set; }
}