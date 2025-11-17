using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.BookingRequest;
using Rooms.Domain.Models.BookingRequests;

namespace Rooms.Core.DtoConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class BookingRequestDtoMapper
{
    public static partial BookingRequestDto MapBookingRequestToDto(BookingRequest bookingRequest);

    public static partial BookingCreatorDto MapBookingCreatorToDto(BookingCreator bookingCreator);

    public static partial BookingCreator MapBookingCreatorFromDto(BookingCreatorDto bookingCreator);

    public static partial BookingTimeDto MapBookingTimeToDto(BookingTime bookingTime);

    public static partial BookingTime MapBookingTimeFromDto(BookingTimeDto bookingTime);
}