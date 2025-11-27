namespace Booking.Core.Interfaces.Dtos.InstituteCoordinator;

public class InstituteCoordinatorDto
{
    public required int Id { get; init; }
    public required string Institute { get; init; }
    public required CoordinatorDto[] Coordinators { get; init; }
}