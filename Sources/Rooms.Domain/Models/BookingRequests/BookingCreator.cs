namespace Rooms.Domain.Models.BookingRequests;

/// <summary>
/// Создатель заявки на бронирование
/// </summary>
public record BookingCreator
{
    /// <summary>
    /// ФИО создателя
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// E-mail создателя
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Номер телефона создателя
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
}