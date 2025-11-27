using Booking.Core.Dtos.InstituteCoordinator;
using Booking.Domain.Models.InstituteCoordinators;
using Riok.Mapperly.Abstractions;

namespace Booking.Core.Services.InstituteCoordinators.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteCoordinatorsDtoMapper
{
    public static partial InstituteCoordinatorDto MapInstituteResponsibleToDto(InstituteCoordinator instituteCoordinator);

    public static partial InstituteCoordinator MapInstituteResponsibleFromDto(InstituteCoordinatorDto instituteCoordinator);
}