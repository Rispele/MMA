using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Spreadsheets.Specifications;

public struct EquipmentRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<EquipmentRegistryExcelExportDto>
{
    private static readonly IReadOnlyList<string> ColumnNamesSpecification =
    [
        "Аудитория",
        "Тип оборудования",
        "Модель оборудования",
        "Инвентарный номер",
        "Серийный номер",
        "Комментарий",
        "Статус",
    ];

    public string SheetName => "Оборудование";
    public string FileName => "Реестр оборудования.xlsx";

    public IReadOnlyList<string> ColumnNames => ColumnNamesSpecification;

    public IEnumerable<ColumnCellData> GetValuesToWrite(EquipmentRegistryExcelExportDto data)
    {
        yield return new ColumnCellData(ColumnNumber: 0, new StringExcelValueType(data.RoomName));
        yield return new ColumnCellData(ColumnNumber: 1, new StringExcelValueType(data.EquipmentType));
        yield return new ColumnCellData(ColumnNumber: 2, new StringExcelValueType(data.EquipmentSchemaName));
        yield return new ColumnCellData(ColumnNumber: 3, new StringExcelValueType(data.InventoryNumber));
        yield return new ColumnCellData(ColumnNumber: 4, new StringExcelValueType(data.SerialNumber));
        yield return new ColumnCellData(ColumnNumber: 5, new StringExcelValueType(data.Comment));
        yield return new ColumnCellData(ColumnNumber: 6, new StringExcelValueType(data.Status));
    }
}