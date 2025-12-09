using WebApi.Core.Models.Requests.Filtering;

namespace WebApi.Core.Models.Requests.InstituteResponsible;

public record InstituteCoordinatorFilterModel
{
    public FilterParameterModel<string>? Institute { get; init; }
    public FilterParameterModel<string>? Responsible { get; init; }
}