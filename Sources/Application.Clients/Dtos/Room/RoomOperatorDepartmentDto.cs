namespace Application.Clients.Dtos.Room;

public record RoomOperatorDepartmentDto(int Id, string Name, string Contacts, RoomOperatorDto[] RoomOperator);
