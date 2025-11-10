namespace Rooms.Core.Dtos.Requests.InstituteResponsible;

public record GetInstituteResponsibleDto(int BatchNumber, int BatchSize, int AfterInstituteResponsibleId, InstituteResponsibleFilterDto? Filter);