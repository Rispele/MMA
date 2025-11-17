namespace Rooms.Domain.Models.InstituteResponsible;

/// <summary>
/// Модель ответственных лиц от института/подразделения
/// </summary>
public class InstituteResponsible
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Институт/подразделение
    /// </summary>
    public required string Institute { get; set; }

    /// <summary>
    /// Ответственные
    /// </summary>
    public required Dictionary<string, string> Responsible { get; set; }

    public void Update(
        string institute,
        Dictionary<string, string> responsible)
    {
        Institute = institute;
        Responsible = responsible;
    }
}