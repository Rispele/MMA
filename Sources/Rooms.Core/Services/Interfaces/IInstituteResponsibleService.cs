using Rooms.Core.Dtos.InstituteResponsible;
using Rooms.Core.Dtos.Requests.InstituteResponsible;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.Interfaces;

public interface IInstituteResponsibleService
{
    Task<InstituteResponsibleDto> GetInstituteResponsibleById(int equipmentId, CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteResponsible(CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteDepartments(CancellationToken cancellationToken);
    Task<InstituteResponsibleResponseDto> FilterInstituteResponsible(GetInstituteResponsibleDto dto, CancellationToken cancellationToken);
    Task<InstituteResponsibleDto> CreateInstituteResponsible(CreateInstituteResponsibleDto dto, CancellationToken cancellationToken);
    Task<InstituteResponsibleDto> PatchInstituteResponsible(int equipmentId, PatchInstituteResponsibleDto dto, CancellationToken cancellationToken);
}