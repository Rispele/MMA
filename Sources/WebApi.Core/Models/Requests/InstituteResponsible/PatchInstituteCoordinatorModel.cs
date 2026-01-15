using WebApi.Core.Models.InstituteCoordinator;

namespace WebApi.Core.Models.Requests.InstituteResponsible;

public record PatchInstituteCoordinatorModel
{
    public required Guid InstituteId { get; init; }
    public required CoordinatorModel[] Coordinators { get; init; }
}