using WebApi.Models.InstituteCoordinator;

namespace WebApi.Models.Requests.InstituteResponsible;

public record PatchInstituteCoordinatorModel
{
    public required string Institute { get; init; }
    public required CoordinatorModel[] Coordinators { get; init; }
}