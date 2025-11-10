using Rooms.Core.Dtos.Requests.Filtering;

namespace Rooms.Core.Dtos.Requests.InstituteResponsible;

public record InstituteResponsibleFilterDto
{
    public FilterParameterDto<string>? Institute { get; init; }
    public FilterParameterDto<string>? Responsible { get; init; }
}