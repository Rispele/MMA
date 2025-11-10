namespace WebApi.Models.InstituteResponsible;

public class InstituteResponsibleModel
{
    public int Id { get; set; }
    public required string Institute { get; set; }
    public required Dictionary<string, string> Responsible { get; set; }
}