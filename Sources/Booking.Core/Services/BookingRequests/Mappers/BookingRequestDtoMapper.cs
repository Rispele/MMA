using Booking.Core.Interfaces.Dtos.BookingRequest;
using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;
using Booking.Domain.Models.BookingRequests;
using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using Riok.Mapperly.Abstractions;

namespace Booking.Core.Services.BookingRequests.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class BookingRequestDtoMapper
{
    [MapProperty(nameof(BookingRequest.RoomEventCoordinator), nameof(BookingRequestDto.RoomEventCoordinator),
        Use = nameof(MapRoomEventCoordinatorToDto))]
    public static partial BookingRequestDto MapBookingRequestToDto(BookingRequest bookingRequest);

    public static partial BookingCreatorDto MapBookingCreatorToDto(BookingCreator bookingCreator);

    public static partial BookingCreator MapBookingCreatorFromDto(BookingCreatorDto bookingCreator);

    public static partial BookingTimeDto MapBookingTimeToDto(BookingTime bookingTime);

    public static BookingTime MapBookingTimeFromDto(BookingTimeDto bookingTime)
    {
        return new BookingTime
        {
            Key = Guid.NewGuid(),
            RoomId = bookingTime.RoomId,
            Date = bookingTime.Date,
            TimeFrom = bookingTime.TimeFrom,
            TimeTo = bookingTime.TimeTo,
            BookingFinishDate = bookingTime.BookingFinishDate,
            Frequency = bookingTime.Frequency
        };
    }

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