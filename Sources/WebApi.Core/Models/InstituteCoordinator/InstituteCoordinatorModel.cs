namespace WebApi.Core.Models.InstituteCoordinator;

public class InstituteCoordinatorModel
{
    public required int Id { get; init; }
    public required Guid InstituteId { get; init; }
    public string InstituteName { get; set; } = null!;
    public required CoordinatorModel[] Coordinators { get; init; }
}