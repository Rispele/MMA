namespace WebApi.Models.Requests.Filtering;

public record FilterParameter<T>
{
    public T? Value { get; init; }
    public SortDirection SortDirection { get; init; }
}