namespace Rooms.Core.Dtos.Requests.InstituteResponsible;

public record PatchInstituteResponsibleDto
{
    public string Institute { get; set; } = null!;
    public Dictionary<string, string> Responsible { get; set; } = null!;
}