using Booking.Core.Interfaces.Dtos.InstituteCoordinator;
using Booking.Domain.Models.InstituteCoordinators;
using Commons.ExternalClients.LkUsers;
using Riok.Mapperly.Abstractions;

namespace Booking.Core.Services.InstituteCoordinators.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteCoordinatorsDtoMapper
{
    [MapperIgnoreTarget(nameof(InstituteCoordinatorDto.InstituteName))]
    public static partial InstituteCoordinatorDto MapInstituteCoordinatorToDto(InstituteCoordinator instituteCoordinator);

    [MapProperty(nameof(LkEmployeeDto.UserId), nameof(InstituteCoordinatorEmployeeDto.UserId))]
    [MapProperty(nameof(LkEmployeeDto.FullName), nameof(InstituteCoordinatorEmployeeDto.FullName))]
    [MapProperty(nameof(LkEmployeeDto.InstituteId), nameof(InstituteCoordinatorEmployeeDto.InstituteId))]
    [MapperIgnoreSource(nameof(LkEmployeeDto.Category))]
    [MapperIgnoreSource(nameof(LkEmployeeDto.PersonId))]
    [MapperIgnoreSource(nameof(LkEmployeeDto.Post))]
    [MapperIgnoreSource(nameof(LkEmployeeDto.Institute))]
    [MapperIgnoreSource(nameof(LkEmployeeDto.TeacherKey))]
    public static partial InstituteCoordinatorEmployeeDto MapInstituteCoordinatorEmployeeToDto(LkEmployeeDto employee);
}