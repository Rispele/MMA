using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.BookingRequest;
using Rooms.Core.Dtos.BookingRequest.RoomEventCoordinator;
using Rooms.Domain.Models.BookingRequests;
using Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Rooms.Core.Services.Booking.BookingRequests.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class BookingRequestDtoMapper
{
    [MapProperty(nameof(BookingRequest.RoomEventCoordinator), nameof(BookingRequestDto.RoomEventCoordinator), Use = nameof(MapRoomEventCoordinatorToDto))]
    public static partial BookingRequestDto MapBookingRequestToDto(BookingRequest bookingRequest);

    public static partial BookingCreatorDto MapBookingCreatorToDto(BookingCreator bookingCreator);

    public static partial BookingCreator MapBookingCreatorFromDto(BookingCreatorDto bookingCreator);

    public static partial BookingTimeDto MapBookingTimeToDto(BookingTime bookingTime);

    public static partial BookingTime MapBookingTimeFromDto(BookingTimeDto bookingTime);

    public static IRoomEventCoordinator MapRoomEventCoordinatorFromDto(IRoomEventCoordinatorDto coordinator)
    {
        return coordinator switch
        {
            OtherRoomEventCoordinatorDto => new OtherRoomEventCoordinator(),
            ScientificRoomEventCoordinatorDto scientificRoomEventCoordinatorDto => new ScientificRoomEventCoordinator(
                scientificRoomEventCoordinatorDto.CoordinatorDepartmentId,
                scientificRoomEventCoordinatorDto.CoordinatorId),
            StudentRoomEventCoordinatorDto => new StudentRoomEventCoordinator(),
            _ => throw new ArgumentOutOfRangeException(nameof(coordinator))
        };
    }
    
    public static IRoomEventCoordinatorDto MapRoomEventCoordinatorToDto(IRoomEventCoordinator coordinator)
    {
        return coordinator switch
        {
            OtherRoomEventCoordinator => new OtherRoomEventCoordinatorDto(),
            ScientificRoomEventCoordinator scientificRoomEventCoordinatorDto => new ScientificRoomEventCoordinatorDto(
                scientificRoomEventCoordinatorDto.CoordinatorDepartmentId,
                scientificRoomEventCoordinatorDto.CoordinatorId),
            StudentRoomEventCoordinator => new StudentRoomEventCoordinatorDto(),
            _ => throw new ArgumentOutOfRangeException(nameof(coordinator))
        };
    }
}