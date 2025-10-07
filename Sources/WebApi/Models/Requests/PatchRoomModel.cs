using System.ComponentModel.DataAnnotations;
using WebApi.Models.Room;

namespace WebApi.Models.Requests;

public class PatchRoomModel
{
    [Required]
    [Length(1, 64, ErrorMessage = "Длина имени должна быть от 1 до 64 символов включительно")]
    [RegularExpression("[a-zA-Zа-яА-Я0-9,\\.\\-#№\\(\\)]{0,64}", ErrorMessage = "В поле Name переданы недоступные символы")]
    public required string Name { get; set; }

    [Length(1, 256, ErrorMessage = "Длина имени должна быть от 1 до 256 символов включительно")]
    public string? Description { get; set; }

    public RoomTypeModel Type { get; set; }
    public RoomLayoutModel Layout { get; set; }
    public RoomNetTypeModel NetType { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? Seats { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Кол-во мест не может быть отрицательным")]
    public int? ComputerSeats { get; set; }

    public bool HasConditioning { get; set; }

    [Length(1, 64, ErrorMessage = "Длина владельца должна быть от 1 до 64 символов включительно")]
    public string? Owner { get; set; }

    public RoomStatusModel RoomStatus { get; set; }

    [Length(1, 256, ErrorMessage = "Длина комментария должна быть от 1 до 256 символов включительно")]
    public string? Comment { get; set; }

    public DateTime? FixDeadline { get; set; }
    public bool AllowBooking { get; set; }
}