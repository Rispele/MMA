using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Core.Dtos.Room;

namespace Rooms.Core.Spreadsheets.ExportModels;

public record RoomRegistrySpreadsheetExportDto
{
    public required RoomDto Room { get; init; }
    public required OperatorDepartmentDto? OperatorDepartment { get; init; }
}