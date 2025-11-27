namespace Commons.Core.Models.Filtering;

public record FilterParameterDto<T>(T Value, SortDirectionDto SortDirection);