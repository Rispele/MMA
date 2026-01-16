using WebApi.Core.Models.Requests.Filtering;

namespace WebApi.Core.Models.Requests.InstituteCoordinators;

public record InstituteCoordinatorsFilterModel
{
    public FilterParameterModel<string>? Institute { get; init; }
    public FilterParameterModel<string>? Responsible { get; init; }
}