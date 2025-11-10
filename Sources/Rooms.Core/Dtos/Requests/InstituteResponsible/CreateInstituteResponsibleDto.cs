namespace Rooms.Core.Dtos.Requests.InstituteResponsible;

public record CreateInstituteResponsibleDto
{
    public string Institute { get; set; } = null!;
    public Dictionary<string, string> Responsible { get; set; } = null!;
}