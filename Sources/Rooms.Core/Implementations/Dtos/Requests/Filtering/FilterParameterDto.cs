namespace Rooms.Core.Implementations.Dtos.Requests.Filtering;

public record FilterParameterDto<T>(T Value, SortDirectionDto SortDirection);
