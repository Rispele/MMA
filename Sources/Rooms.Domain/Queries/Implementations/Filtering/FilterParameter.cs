namespace Rooms.Domain.Queries.Implementations.Filtering;

public record FilterParameter<T>(T Value, SortDirection SortDirection);
