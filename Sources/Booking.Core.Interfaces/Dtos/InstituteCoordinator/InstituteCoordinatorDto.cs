namespace Booking.Core.Interfaces.Dtos.InstituteCoordinator;

public class InstituteCoordinatorDto
{
    public required int Id { get; init; }
    public required Guid InstituteId { get; init; }
    public string InstituteName { get; set; } = null!;
    public required CoordinatorDto[] Coordinators { get; init; }
}