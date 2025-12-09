namespace WebApi.Core.Models.Requests.OperatorDepartments;

public record CreateOperatorDepartmentModel
{
    public required string Name { get; set; }
    public int[] RoomIds { get; set; } = [];
    public Dictionary<string, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}