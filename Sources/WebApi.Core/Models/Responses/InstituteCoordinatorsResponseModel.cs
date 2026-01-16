using WebApi.Core.Models.InstituteCoordinator;

namespace WebApi.Core.Models.Responses;

public record InstituteCoordinatorsResponseModel(InstituteCoordinatorModel[] InstituteCoordinators, int TotalCount);