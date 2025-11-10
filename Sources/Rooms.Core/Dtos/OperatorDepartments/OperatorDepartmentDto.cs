namespace Rooms.Core.Dtos.OperatorDepartments;

public class OperatorDepartmentDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required OperatorDepartmentRoomInfoDto[] Rooms { get; set; }
    public Dictionary<string, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}