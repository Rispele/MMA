using Rooms.Core.Dtos.Filtering;

namespace Rooms.Core.Dtos.InstituteCoordinator.Requests;

public record InstituteCoordinatorFilterDto
{
    public FilterParameterDto<string>? Institute { get; init; }
    public FilterParameterDto<string>? Responsible { get; init; }
}