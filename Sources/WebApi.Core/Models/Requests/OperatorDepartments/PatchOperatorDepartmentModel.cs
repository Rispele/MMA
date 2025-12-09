namespace WebApi.Core.Models.Requests.OperatorDepartments;

public record PatchOperatorDepartmentModel
{
    public required string Name { get; set; }
    public IEnumerable<int> RoomIds { get; set; } = [];
    public Dictionary<string, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}