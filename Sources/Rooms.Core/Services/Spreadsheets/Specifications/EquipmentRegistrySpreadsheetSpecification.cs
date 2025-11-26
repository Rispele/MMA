using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Core.Services.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Services.Spreadsheets.ExportModels;

namespace Rooms.Core.Services.Spreadsheets.Specifications;

public struct EquipmentRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<EquipmentRegistrySpreadsheetExportDto>
{
    private static readonly IReadOnlyList<ColumnSpecification<EquipmentRegistrySpreadsheetExportDto>> Specifications =
    [
        new(Name: "Аудитория", data => new StringSpreadsheetValueType(data.RoomName)),
        new(Name: "Тип оборудования", data => new StringSpreadsheetValueType(data.TypeName)),
        new(Name: "Модель оборудования", data => new StringSpreadsheetValueType(data.SchemaName)),
        new(Name: "Инвентарный номер", data => new StringSpreadsheetValueType(data.InventoryNumber)),
        new(Name: "Серийный номер", data => new StringSpreadsheetValueType(data.SerialNumber)),
        new(Name: "Комментарий", data => new StringSpreadsheetValueType(data.Comment)),
        new(Name: "Статус", data => new StringSpreadsheetValueType(data.Status))
    ];

    public string SheetName => "Оборудование";
    public string FileName => "Реестр оборудования.xlsx";
    public IReadOnlyList<ColumnSpecification<EquipmentRegistrySpreadsheetExportDto>> ColumnSpecifications => Specifications;
}