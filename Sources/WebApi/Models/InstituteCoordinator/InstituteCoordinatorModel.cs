namespace WebApi.Models.InstituteCoordinator;

public class InstituteCoordinatorModel
{
    public required int Id { get; init; }
    public required string Institute { get; init; }
    public required CoordinatorModel[] Coordinators { get; init; }
}