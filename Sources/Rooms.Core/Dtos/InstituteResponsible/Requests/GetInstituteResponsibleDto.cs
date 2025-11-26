namespace Rooms.Core.Dtos.InstituteResponsible.Requests;

public record GetInstituteResponsibleDto(int BatchNumber, int BatchSize, int AfterInstituteResponsibleId, InstituteResponsibleFilterDto? Filter);