namespace Commons.Core.Models.Filtering;

public record FilterMultiParameterDto<T>(T[] Values, SortDirectionDto SortDirection);