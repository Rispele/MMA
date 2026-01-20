using System.ComponentModel.DataAnnotations;
using Rooms.Core.Interfaces.Dtos.Files;
using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Core.Interfaces.Dtos.Room.Requests;

public record CreateRoomDto
{
    [Required]
    [Length(minimumLength: 1, maximumLength: 64, ErrorMessage = "Длина имени должна быть от 1 до 64 символов включительно")]
    [RegularExpression("[a-zA-Zа-яА-Я0-9,\\.\\-#№\\(\\)]{0,64}",
        ErrorMessage = "В поле Name переданы недоступные символы")]
    public string Name { get; init; } = string.Empty;

    [Length(minimumLength: 1, maximumLength: 256, ErrorMessage = "Длина описания должна быть от 1 до 256 символов включительно")]
    public string? Description { get; init; }

    [Required]
    [Length(minimumLength: 1, maximumLength: 64, ErrorMessage = "Длина номера аудитории должна быть от 1 до 64 символов включительно")]
    public string RoomNumber { get; init; } = null!;

    [Required]
    [Length(minimumLength: 1, maximumLength: 64, ErrorMessage = "Длина адреса аудитории должна быть от 1 до 64 символов включительно")]
    public string Address { get; init; } = null!;

    public RoomType Type { get; init; }
    public RoomLayout Layout { get; init; }

    [Range(minimum: 0, int.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? Seats { get; init; }

    [Range(minimum: 0, int.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? ComputerSeats { get; init; }

    public FileDescriptorDto? PdfRoomSchemeFile { get; init; }

    public FileDescriptorDto? PhotoFile { get; init; }

    public RoomNetType NetType { get; init; }
    public bool HasConditioning { get; init; }

    [Length(minimumLength: 1, maximumLength: 64, ErrorMessage = "Длина владельца должна быть от 1 до 64 символов включительно")]
    public string? Owner { get; init; }

    public RoomStatus RoomStatus { get; init; }

    [Length(minimumLength: 1, maximumLength: 256, ErrorMessage = "Длина комментария должна быть от 1 до 256 символов включительно")]
    public string? Comment { get; init; }

    public DateTime? FixDeadline { get; init; }
    public bool AllowBooking { get; init; }
}