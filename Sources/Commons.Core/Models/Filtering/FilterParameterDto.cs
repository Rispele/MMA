namespace Rooms.Core.Dtos.Filtering;

public record FilterParameterDto<T>(T Value, SortDirectionDto SortDirection);