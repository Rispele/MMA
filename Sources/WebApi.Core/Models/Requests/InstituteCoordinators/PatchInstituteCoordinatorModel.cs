using WebApi.Core.Models.InstituteCoordinator;

namespace WebApi.Core.Models.Requests.InstituteCoordinators;

public record PatchInstituteCoordinatorModel
{
    public required Guid InstituteId { get; init; }
    public required CoordinatorModel[] Coordinators { get; init; }
}