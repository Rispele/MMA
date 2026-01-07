using Booking.Core.Interfaces.Dtos.InstituteCoordinator;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Responses;
using Commons.ExternalClients.InstituteDepartments;
using Commons.ExternalClients.LkUsers;

namespace Booking.Core.Interfaces.Services.InstituteCoordinators;

public interface IInstituteCoordinatorsService
{
    Task<InstituteCoordinatorDto> GetInstituteResponsibleById(int equipmentId, CancellationToken cancellationToken);
    Task<LkEmployeeDto[]> GetAvailableInstituteResponsible(CancellationToken cancellationToken);
    Task<InstituteDepartmentResponseDto[]> GetAvailableInstituteDepartments(CancellationToken cancellationToken);
    Task<InstituteCoordinatorResponseDto> FilterInstituteResponsible(GetInstituteCoordinatorDto dto, CancellationToken cancellationToken);
    Task<InstituteCoordinatorDto> CreateInstituteResponsible(CreateInstituteCoordinatorDto dto, CancellationToken cancellationToken);
    Task<InstituteCoordinatorDto> PatchInstituteResponsible(int equipmentId, PatchInstituteCoordinatorDto dto, CancellationToken cancellationToken);
}