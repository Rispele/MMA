using Booking.Core.Interfaces.Dtos.InstituteCoordinator;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Responses;
using Commons.ExternalClients.InstituteDepartments;

namespace Booking.Core.Interfaces.Services.InstituteCoordinators;

public interface IInstituteCoordinatorService
{
    Task<InstituteCoordinatorDto> GetInstituteCoordinatorById(int equipmentId, CancellationToken cancellationToken);
    Task<InstituteCoordinatorEmployeeDto[]> GetAvailableInstituteCoordinators(CancellationToken cancellationToken);
    Task<InstituteDepartmentResponseDto[]> GetAvailableInstituteDepartments(CancellationToken cancellationToken);
    Task<InstituteCoordinatorsResponseDto> FilterInstituteCoordinators(GetInstituteCoordinatorDto dto, CancellationToken cancellationToken);
    Task<InstituteCoordinatorDto> CreateInstituteCoordinator(CreateInstituteCoordinatorDto dto, CancellationToken cancellationToken);
    Task<InstituteCoordinatorDto> PatchInstituteCoordinator(int equipmentId, PatchInstituteCoordinatorDto dto, CancellationToken cancellationToken);
}