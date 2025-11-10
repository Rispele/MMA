namespace WebApi.Models.Requests.InstituteResponsible;

public record PatchInstituteResponsibleModel
{
    public string Institute { get; set; } = null!;
    public Dictionary<string, string> Responsible { get; set; } = null!;
}