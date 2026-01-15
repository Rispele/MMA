using WebApi.Core.Models.InstituteCoordinator;

namespace WebApi.Core.Models.Requests.InstituteResponsible;

public record CreateInstituteCoordinatorModel
{
    public required Guid InstituteId { get; init; }
    public required CoordinatorModel[] Coordinators { get; init; }
}