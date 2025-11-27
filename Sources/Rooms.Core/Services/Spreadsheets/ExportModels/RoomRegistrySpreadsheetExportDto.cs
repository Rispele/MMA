using Rooms.Core.Interfaces.Dtos.OperatorDepartments;
using Rooms.Core.Interfaces.Dtos.Room;

namespace Rooms.Core.Services.Spreadsheets.ExportModels;

internal record RoomRegistrySpreadsheetExportDto
{
    public required RoomDto Room { get; init; }
    public required OperatorDepartmentDto? OperatorDepartment { get; init; }
}