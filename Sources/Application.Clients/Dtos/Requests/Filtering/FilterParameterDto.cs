namespace Application.Clients.Dtos.Requests.Filtering;

public record FilterParameterDto<T>(T Value, SortDirectionDto SortDirectionDto);