namespace Application.Implementations.Dtos.Requests.Filtering;

public record FilterParameter<T>
{
    public T? Value { get; init; }
    public SortDirection SortDirection { get; init; }
}