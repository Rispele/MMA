using WebApi.Core.Models.InstituteCoordinator;

namespace WebApi.Core.Models.Responses;

public record InstituteResponsibleResponseModel
{
    public InstituteCoordinatorModel[] InstituteResponsible { get; init; } = [];
    public int Count { get; init; }
}