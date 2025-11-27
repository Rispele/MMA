namespace Rooms.Core.Dtos.OperatorDepartments.Requests;

public record CreateOperatorDepartmentDto
{
    public required string Name { get; set; }
    public int[] RoomIds { get; set; } = [];
    public Dictionary<string, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}