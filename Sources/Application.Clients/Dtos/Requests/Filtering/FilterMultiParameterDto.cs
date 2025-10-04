namespace Application.Clients.Dtos.Requests.Filtering;

public record FilterMultiParameterDto<T>
{
    public T[] Values { get; init; } = null!;
    public SortDirectionDto SortDirectionDto { get; init; }
}