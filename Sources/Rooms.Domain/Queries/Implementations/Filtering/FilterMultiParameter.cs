namespace Rooms.Domain.Queries.Implementations.Filtering;

public record FilterMultiParameter<T>(T[] Values, SortDirection SortDirection);