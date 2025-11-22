using Commons;
using Rooms.Core.Dtos.Room.Fix;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Spreadsheets.ExportModels;

namespace Rooms.Core.Spreadsheets.Specifications;

//todo: смапить все енумы в строки
public struct RoomRegistrySpreadsheetSpecification
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

    private static string MapFixStatus(RoomStatusDto fixStatus)
    {
        return fixStatus switch {
            RoomStatusDto.Unspecified => "Не указано",
            RoomStatusDto.Ready => "Готова",
            RoomStatusDto.PartiallyReady => "Частично готова",
            RoomStatusDto.Malfunction => "Ничего не работает",
            _ => throw new ArgumentOutOfRangeException(nameof(fixStatus), fixStatus, null)
        };
    }
}