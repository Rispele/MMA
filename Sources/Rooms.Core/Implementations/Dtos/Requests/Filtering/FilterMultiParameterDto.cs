namespace Rooms.Core.Implementations.Dtos.Requests.Filtering;

public record FilterMultiParameterDto<T>(T[] Values, SortDirectionDto SortDirection);
