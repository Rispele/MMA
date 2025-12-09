namespace WebApi.Core.Models.OperatorDepartments;

public class OperatorDepartmentModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required OperatorDepartmentRoomInfoModel[] Rooms { get; set; }
    public Dictionary<string, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}