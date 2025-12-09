namespace WebApi.Core.Models.Files;

public record FileExportModel
{
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required Action Flush { get; init; }
}