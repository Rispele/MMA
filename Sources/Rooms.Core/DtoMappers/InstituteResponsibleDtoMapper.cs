using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.InstituteResponsible;
using Rooms.Domain.Models.InstituteResponsibles;

namespace Rooms.Core.DtoMappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteResponsibleDtoMapper
{
    public static partial InstituteResponsibleDto MapInstituteResponsibleToDto(InstituteResponsible instituteResponsible);

    public static partial InstituteResponsible MapInstituteResponsibleFromDto(InstituteResponsibleDto instituteResponsible);
}