using WebApi.Models.Requests.Filtering;

namespace WebApi.Models.Requests.InstituteResponsible;

public record InstituteResponsibleFilterModel
{
    public FilterParameterModel<string>? Institute { get; init; }
    public FilterParameterModel<string>? Responsible { get; init; }
}