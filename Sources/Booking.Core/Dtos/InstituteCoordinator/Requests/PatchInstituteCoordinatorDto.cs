namespace Booking.Core.Dtos.InstituteCoordinator.Requests;

public record PatchInstituteCoordinatorDto
{
    public string Institute { get; set; } = null!;
    public CoordinatorDto[] Coordinators { get; set; } = null!;
}