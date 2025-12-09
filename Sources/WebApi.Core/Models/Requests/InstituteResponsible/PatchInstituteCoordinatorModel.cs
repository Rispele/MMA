using WebApi.Core.Models.InstituteCoordinator;

namespace WebApi.Core.Models.Requests.InstituteResponsible;

public record PatchInstituteCoordinatorModel
{
    public required string Institute { get; init; }
    public required CoordinatorModel[] Coordinators { get; init; }
}