using Commons;
using EnumsNET;
using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Core.Services.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Services.Spreadsheets.ExportModels;

namespace Rooms.Core.Services.Spreadsheets.Specifications;

internal struct RoomRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<RoomRegistrySpreadsheetExportDto>
{
    private static readonly IReadOnlyList<ColumnSpecification<RoomRegistrySpreadsheetExportDto>> Specifications =
    [
        new(Name: "Аудитория", data => new StringSpreadsheetValueType(data.Room.Name)),
        new(Name: "Краткое описание", data => new StringSpreadsheetValueType(data.Room.Description)),
        new(Name: "Тип", data => new StringSpreadsheetValueType(data.Room.Parameters.Type.AsString(EnumFormat.Description))),
        new(Name: "Планировка", data => new StringSpreadsheetValueType(data.Room.Parameters.Layout.AsString(EnumFormat.Description))),
        new(Name: "Вместимость", data => new StringSpreadsheetValueType(data.Room.Parameters.Seats.ToString())),
        new(Name: "Вместимость с ПК", data => new StringSpreadsheetValueType(data.Room.Parameters.ComputerSeats.ToString())),
        new(Name: "Сеть", data => new StringSpreadsheetValueType(data.Room.Parameters.NetType.AsString(EnumFormat.Description))),
        new(Name: "Кондиционирование", data => new StringSpreadsheetValueType(data.Room.Parameters.HasConditioning is true ? "Есть" : "Нет")),
        new(Name: "Операторская", data => new StringSpreadsheetValueType(data.OperatorDepartment?.Name)),
        new(Name: "Операторы", data => new StringSpreadsheetValueType(data.OperatorDepartment?.Operators.Keys.JoinStrings(", "))),
        new(Name: "Владелец", data => new StringSpreadsheetValueType(data.Room.Owner)),
        new(Name: "Степень готовности", data => new StringSpreadsheetValueType(data.Room.FixInfo.Status.AsString(EnumFormat.Description))),
        new(Name: "Комментарий", data => new StringSpreadsheetValueType(data.Room.FixInfo.Comment)),
        new(Name: "Крайний срок исправлений", data => new StringSpreadsheetValueType(data.Room.FixInfo.FixDeadline.ToString())),
    ];

    public string SheetName => "Аудитории";
    public string FileName => "Реестр аудиторий.xlsx";
    public IReadOnlyList<ColumnSpecification<RoomRegistrySpreadsheetExportDto>> ColumnSpecifications => Specifications;
}