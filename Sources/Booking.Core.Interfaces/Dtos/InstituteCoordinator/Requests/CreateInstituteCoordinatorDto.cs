namespace Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;

public record CreateInstituteCoordinatorDto
{
    public Guid InstituteId { get; set; }
    public CoordinatorDto[] Coordinators { get; set; } = null!;
}