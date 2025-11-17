namespace Rooms.Core.Dtos.Room;

public class RoomRegistryExcelExportDto
{
    public required string Name { get; set; } = null!;
    public string? RoomType { get; set; } = null!;
    public int? Seats { get; set; }
    public int? ComputerSeats { get; set; }
    public string? NetworkEquipmentIp { get; set; }
    public string? OperatorDepartmentName { get; set; }
    public string? Owner { get; set; }
    public string? Status { get; set; }
}