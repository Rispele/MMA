namespace Rooms.Core.Interfaces.Dtos.OperatorDepartments;

public class OperatorDepartmentDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required OperatorDepartmentRoomInfoDto[] Rooms { get; init; }
    public required IReadOnlyDictionary<string, string> Operators { get; init; }
    public required string Contacts { get; init; } = null!;
}