using Rooms.Core.Clients.LkUsers;
using Rooms.Core.Dtos.InstituteResponsible;
using Rooms.Core.Dtos.InstituteResponsible.Requests;
using Rooms.Core.Dtos.InstituteResponsible.Responses;

namespace Rooms.Core.Services.InstituteResponsibles.Interfaces;

public interface IInstituteResponsibleService
{
    Task<InstituteResponsibleDto> GetInstituteResponsibleById(int equipmentId, CancellationToken cancellationToken);
    Task<LkEmployeeDto[]> GetAvailableInstituteResponsible(CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteDepartments(CancellationToken cancellationToken);
    Task<InstituteResponsibleResponseDto> FilterInstituteResponsible(GetInstituteResponsibleDto dto, CancellationToken cancellationToken);
    Task<InstituteResponsibleDto> CreateInstituteResponsible(CreateInstituteResponsibleDto dto, CancellationToken cancellationToken);
    Task<InstituteResponsibleDto> PatchInstituteResponsible(int equipmentId, PatchInstituteResponsibleDto dto, CancellationToken cancellationToken);
}