using WebApi.Core.Models.Requests.Filtering;

namespace WebApi.Core.Models.Requests.InstituteCoordinators;

public record InstituteCoordinatorsFilterModel
{
    public FilterParameterModel<Guid>? InstituteId { get; init; }
    public FilterParameterModel<string>? Coordinator { get; init; }
}