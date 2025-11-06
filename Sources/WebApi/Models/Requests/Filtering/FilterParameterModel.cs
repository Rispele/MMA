namespace WebApi.Models.Requests.Filtering;

public record FilterParameterModel<T>
{
    public required T Value { get; init; }
    public SortDirectionModel SortDirection { get; init; }
}