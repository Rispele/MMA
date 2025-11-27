using Commons.Core.Models.Filtering;

namespace Booking.Core.Dtos.InstituteCoordinator.Requests;

public record InstituteCoordinatorFilterDto
{
    public FilterParameterDto<string>? Institute { get; init; }
    public FilterParameterDto<string>? Responsible { get; init; }
}