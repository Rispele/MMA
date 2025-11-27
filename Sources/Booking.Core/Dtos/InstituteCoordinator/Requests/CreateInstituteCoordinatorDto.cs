namespace Rooms.Core.Dtos.InstituteCoordinator.Requests;

public record CreateInstituteCoordinatorDto
{
    public string Institute { get; set; } = null!;
    public CoordinatorDto[] Coordinators { get; set; } = null!;
}