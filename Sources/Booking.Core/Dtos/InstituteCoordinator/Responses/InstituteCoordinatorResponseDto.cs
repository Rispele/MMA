namespace Booking.Core.Dtos.InstituteCoordinator.Responses;

public record InstituteCoordinatorResponseDto(InstituteCoordinatorDto[] InstituteResponsible, int Count, int? LastInstituteResponsibleId);