namespace Rooms.Core.Dtos.Room.Parameters;

public record RoomParametersDto(
    RoomTypeDto Type,
    RoomLayoutDto Layout,
    RoomNetTypeDto NetType,
    int? Seats,
    int? ComputerSeats,
    bool? HasConditioning);