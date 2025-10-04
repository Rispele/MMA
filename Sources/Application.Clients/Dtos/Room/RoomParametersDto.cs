namespace Application.Clients.Dtos.Room;

public record RoomParametersDto(
    RoomTypeDto Type,
    RoomLayoutDto Layout,
    RoomNetTypeDto NetType, 
    int? Seats, 
    int? ComputerSeats,
    bool? HasConditioning);