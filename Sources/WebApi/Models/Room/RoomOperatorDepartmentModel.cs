namespace WebApi.Models.Room;

public record RoomOperatorDepartmentModel(int Id, string Name, string Contacts, RoomOperatorModel[] RoomOperator);