namespace Rooms.Core.Dtos.InstituteResponsible;

public class InstituteResponsibleDto
{
    public int Id { get; set; }
    public required string Institute { get; set; }
    public required Dictionary<string, string> Responsible { get; set; }
}