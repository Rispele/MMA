namespace Application.Clients.Dtos.Requests.Filtering;

public record FilterMultiParameterDto<T>(T[] Values, SortDirectionDto SortDirection);
