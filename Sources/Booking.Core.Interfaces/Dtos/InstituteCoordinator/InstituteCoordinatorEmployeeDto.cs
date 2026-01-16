namespace Booking.Core.Interfaces.Dtos.InstituteCoordinator;

public class InstituteCoordinatorEmployeeDto
{
    public string UserId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public Guid InstituteId { get; set; }
}