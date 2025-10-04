namespace Application.Clients.Dtos.Requests.Filtering;

public record FilterParameterDto<T>
{
    public required T Value { get; init; }
    public SortDirectionDto SortDirectionDto { get; init; }
}