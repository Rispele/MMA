using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Core.Interfaces.Dtos.Room.Parameters;

public record RoomParametersDto(
    RoomType Type,
    RoomLayout Layout,
    RoomNetType NetType,
    int? Seats,
    int? ComputerSeats,
    bool? HasConditioning);