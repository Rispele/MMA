using Rooms.Core.Dtos.InstituteResponsible;

namespace Rooms.Core.Dtos.Responses;

public record InstituteResponsibleResponseDto(InstituteResponsibleDto[] InstituteResponsible, int Count, int? LastInstituteResponsibleId);