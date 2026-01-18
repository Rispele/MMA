using Commons.Core.Models.Filtering;

namespace Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;

public record InstituteCoordinatorFilterDto
{
    public FilterParameterDto<Guid>? InstituteId { get; init; }
    public FilterParameterDto<string>? Coordinator { get; init; }
}