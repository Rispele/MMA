using Commons;
using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Core.Services.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Services.Spreadsheets.ExportModels;
using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Core.Services.Spreadsheets.Specifications;

//todo: смапить все енумы в строки
internal struct RoomRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<RoomRegistrySpreadsheetExportDto>
{
    private static readonly IReadOnlyList<ColumnSpecification<RoomRegistrySpreadsheetExportDto>> Specifications =
    [
        new(Name: "Аудитория", data => new StringSpreadsheetValueType(data.Room.Name)),
        new(Name: "Краткое описание", data => new StringSpreadsheetValueType(data.Room.Description)),
        new(Name: "Тип", data => new StringSpreadsheetValueType(data.Room.Parameters.Type.ToString())),
        new(Name: "Планировка", data => new StringSpreadsheetValueType(data.Room.Parameters.Layout.ToString())),
        new(Name: "Вместимость", data => new StringSpreadsheetValueType(data.Room.Parameters.Seats.ToString())),
        new(Name: "Вместимость с ПК", data => new StringSpreadsheetValueType(data.Room.Parameters.ComputerSeats.ToString())),
        new(Name: "Сеть", data => new StringSpreadsheetValueType(data.Room.Parameters.NetType.ToString())),
        new(Name: "Кондиционирование", data => new StringSpreadsheetValueType(data.Room.Parameters.HasConditioning is true ? "Есть" : "Нет")),
        new(Name: "Операторская", data => new StringSpreadsheetValueType(data.OperatorDepartment?.Name)),
        new(Name: "Операторы", data => new StringSpreadsheetValueType(data.OperatorDepartment?.Operators.Keys.JoinStrings(", "))),
        new(Name: "Владелец", data => new StringSpreadsheetValueType(data.Room.Owner)),
        new(Name: "Степень готовности", data => new StringSpreadsheetValueType(MapFixStatus(data.Room.FixInfo.Status))),
        new(Name: "Комментарий", data => new StringSpreadsheetValueType(data.Room.FixInfo.Comment)),
        new(Name: "Крайний срок исправлений", data => new StringSpreadsheetValueType(data.Room.FixInfo.FixDeadline.ToString())),
    ];

    public string SheetName => "Аудитории";
    public string FileName => "Реестр аудиторий.xlsx";
    public IReadOnlyList<ColumnSpecification<RoomRegistrySpreadsheetExportDto>> ColumnSpecifications => Specifications;

    private static string MapFixStatus(RoomStatus fixStatus)
    {
        return fixStatus switch {
            RoomStatus.Unspecified => "Не указано",
            RoomStatus.Ready => "Готова",
            RoomStatus.PartiallyReady => "Частично готова",
            RoomStatus.Malfunction => "Ничего не работает",
            _ => throw new ArgumentOutOfRangeException(nameof(fixStatus), fixStatus, null)
        };
    }
}