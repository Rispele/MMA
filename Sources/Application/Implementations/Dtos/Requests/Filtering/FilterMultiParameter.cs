namespace Application.Implementations.Dtos.Requests.Filtering;

public record FilterMultiParameter<T>
{
    public T[]? Values { get; init; }
    public SortDirection SortDirection { get; init; }
}