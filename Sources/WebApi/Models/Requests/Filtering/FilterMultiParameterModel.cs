namespace WebApi.Models.Requests.Filtering;

public record FilterMultiParameterModel<T>
{
    public required T[] Values { get; init; }
    public SortDirectionModel SortDirection { get; init; }
}