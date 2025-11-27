namespace Rooms.Core.Dtos.InstituteCoordinator.Requests;

public record GetInstituteCoordinatorDto(int BatchNumber, int BatchSize, int AfterInstituteResponsibleId, InstituteCoordinatorFilterDto? Filter);