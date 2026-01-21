using System.ComponentModel.DataAnnotations;
using Rooms.Domain.Propagated.Rooms;
using WebApi.Core.Models.Files;
using WebApi.Core.Models.Room;

namespace WebApi.Core.Models.Requests.Rooms;

public class PatchRoomModel
{
    [Required]
    [Length(minimumLength: 1, maximumLength: 64, ErrorMessage = "Длина имени должна быть от 1 до 64 символов включительно")]
    [RegularExpression(" [a-zA-Zа-яА-Я0-9,\\.\\-#№\\(\\)]{0,64}",
        ErrorMessage = "В поле Name переданы недоступные символы")]
    public required string Name { get; set; }

    [Length(minimumLength: 1, maximumLength: 256, ErrorMessage = "Длина имени должна быть от 1 до 256 символов включительно")]
    public string? Description { get; set; }

    public ScheduleAddressModel? ScheduleAddress { get; set; }

    public RoomType Type { get; set; }
    public RoomLayout Layout { get; set; }
    public RoomNetType NetType { get; set; }

    [Range(minimum: 0, int.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? Seats { get; set; }

    [Range(minimum: 0, int.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? ComputerSeats { get; set; }

    public FileDescriptorModel? PdfRoomSchemeFile { get; init; }
    public FileDescriptorModel? PhotoFile { get; init; }
    public bool? HasConditioning { get; set; }

    [Length(minimumLength: 1, maximumLength: 64, ErrorMessage = "Длина владельца должна быть от 1 до 64 символов включительно")]
    public string? Owner { get; set; }

    public RoomStatus RoomStatus { get; set; }

    [Length(minimumLength: 1, maximumLength: 256, ErrorMessage = "Длина комментария должна быть от 1 до 256 символов включительно")]
    public string? Comment { get; set; }

    public DateTime? FixDeadline { get; set; }
    public bool AllowBooking { get; set; }
}