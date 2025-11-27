using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.InstituteCoordinator;
using Rooms.Domain.Models.InstituteCoordinators;

namespace Rooms.Core.Services.InstituteCoordinators.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteResponsibleDtoMapper
{
    public static partial InstituteCoordinatorDto MapInstituteResponsibleToDto(InstituteCoordinator instituteCoordinator);

    public static partial InstituteCoordinator MapInstituteResponsibleFromDto(InstituteCoordinatorDto instituteCoordinator);
}