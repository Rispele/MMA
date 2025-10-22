namespace Rooms.Core.Dtos.Requests.Filtering;

public record FilterMultiParameterDto<T>(T[] Values, SortDirectionDto SortDirection);
