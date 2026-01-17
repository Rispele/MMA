namespace Booking.Core.Interfaces.Dtos.InstituteCoordinator.Responses;

public record InstituteCoordinatorsResponseDto(InstituteCoordinatorDto[] InstituteCoordinators, int TotalCount);