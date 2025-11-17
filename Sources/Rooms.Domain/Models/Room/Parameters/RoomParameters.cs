namespace Rooms.Domain.Models.Room.Parameters;

/// <summary>
/// Параметры аудитории
/// </summary>
public class RoomParameters
{
    /// <summary>
    /// Тип аудитории
    /// </summary>
    public RoomType Type { get; set; }

    /// <summary>
    /// Планировка
    /// </summary>
    public RoomLayout Layout { get; set; }

    /// <summary>
    /// Тип сети
    /// </summary>
    public RoomNetType NetType { get; set; }

    /// <summary>
    /// Вместительность
    /// </summary>
    public int? Seats { get; set; }

    /// <summary>
    /// Вместительность с ПК
    /// </summary>
    public int? ComputerSeats { get; set; }

    /// <summary>
    /// Кондиционирование
    /// </summary>
    public bool? HasConditioning { get; set; }
}