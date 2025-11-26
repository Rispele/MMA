namespace Rooms.Core.Dtos.InstituteResponsible.Requests;

public record CreateInstituteResponsibleDto
{
    public string Institute { get; set; } = null!;
    public Dictionary<string, string> Responsible { get; set; } = null!;
}