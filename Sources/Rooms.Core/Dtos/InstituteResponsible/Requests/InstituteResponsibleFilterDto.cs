using Rooms.Core.Dtos.Filtering;

namespace Rooms.Core.Dtos.InstituteResponsible.Requests;

public record InstituteResponsibleFilterDto
{
    public FilterParameterDto<string>? Institute { get; init; }
    public FilterParameterDto<string>? Responsible { get; init; }
}