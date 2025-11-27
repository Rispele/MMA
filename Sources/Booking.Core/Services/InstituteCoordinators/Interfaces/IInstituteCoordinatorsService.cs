using Booking.Core.Dtos.InstituteCoordinator;
using Booking.Core.Dtos.InstituteCoordinator.Requests;
using Booking.Core.Dtos.InstituteCoordinator.Responses;
using Commons.ExternalClients.LkUsers;

namespace Booking.Core.Services.InstituteCoordinators.Interfaces;

public interface IInstituteCoordinatorsService
{
    Task<InstituteCoordinatorDto> GetInstituteResponsibleById(int equipmentId, CancellationToken cancellationToken);
    Task<LkEmployeeDto[]> GetAvailableInstituteResponsible(CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteDepartments(CancellationToken cancellationToken);
    Task<InstituteCoordinatorResponseDto> FilterInstituteResponsible(GetInstituteCoordinatorDto dto, CancellationToken cancellationToken);
    Task<InstituteCoordinatorDto> CreateInstituteResponsible(CreateInstituteCoordinatorDto dto, CancellationToken cancellationToken);
    Task<InstituteCoordinatorDto> PatchInstituteResponsible(int equipmentId, PatchInstituteCoordinatorDto dto, CancellationToken cancellationToken);
}