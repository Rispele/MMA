namespace Rooms.Core.Dtos.Filtering;

public record FilterMultiParameterDto<T>(T[] Values, SortDirectionDto SortDirection);