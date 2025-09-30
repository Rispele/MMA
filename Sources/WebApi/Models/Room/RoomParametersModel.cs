namespace WebApi.Dto.Room;

public record RoomParametersModel(RoomTypeModel Type, RoomLayoutModel Layout, RoomNetTypeModel NetType, int Seats, int ComputerSeats, bool HasConditioning);