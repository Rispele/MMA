namespace WebApi.Models.Requests.Filtering;

public record FilterParameterModel<T>
{
    public T? Value { get; init; }
    public SortDirection SortDirection { get; init; }
}