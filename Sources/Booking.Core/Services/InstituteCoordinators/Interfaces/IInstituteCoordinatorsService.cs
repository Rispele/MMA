using Rooms.Core.Clients.LkUsers;
using Rooms.Core.Dtos.InstituteCoordinator;
using Rooms.Core.Dtos.InstituteCoordinator.Requests;
using Rooms.Core.Dtos.InstituteCoordinator.Responses;

namespace Rooms.Core.Services.InstituteCoordinators.Interfaces;

public interface IInstituteCoordinatorsService
{
    Task<InstituteCoordinatorDto> GetInstituteResponsibleById(int equipmentId, CancellationToken cancellationToken);
    Task<LkEmployeeDto[]> GetAvailableInstituteResponsible(CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteDepartments(CancellationToken cancellationToken);
    Task<InstituteCoordinatorResponseDto> FilterInstituteResponsible(GetInstituteCoordinatorDto dto, CancellationToken cancellationToken);
    Task<InstituteCoordinatorDto> CreateInstituteResponsible(CreateInstituteCoordinatorDto dto, CancellationToken cancellationToken);
    Task<InstituteCoordinatorDto> PatchInstituteResponsible(int equipmentId, PatchInstituteCoordinatorDto dto, CancellationToken cancellationToken);
}