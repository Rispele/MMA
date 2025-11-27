using WebApi.Models.InstituteCoordinator;

namespace WebApi.Models.Responses;

public record InstituteResponsibleResponseModel
{
    public InstituteCoordinatorModel[] InstituteResponsible { get; init; } = [];
    public int Count { get; init; }
}