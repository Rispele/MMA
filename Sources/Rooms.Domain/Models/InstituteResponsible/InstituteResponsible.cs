namespace Rooms.Domain.Models.InstituteResponsible;

public class InstituteResponsible
{
    public int Id { get; set; }
    public required string Institute { get; set; }
    public required Dictionary<string, string> Responsible { get; set; }

    public void Update(
        string institute,
        Dictionary<string, string> responsible)
    {
        Institute = institute;
        Responsible = responsible;
    }
}