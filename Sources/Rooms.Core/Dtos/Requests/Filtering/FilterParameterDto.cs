namespace Rooms.Core.Dtos.Requests.Filtering;

public record FilterParameterDto<T>(T Value, SortDirectionDto SortDirection);
