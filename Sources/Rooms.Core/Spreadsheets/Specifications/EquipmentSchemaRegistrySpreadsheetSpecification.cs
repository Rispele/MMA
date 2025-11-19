using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Spreadsheets.Specifications;

public struct EquipmentSchemaRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<EquipmentSchemaRegistryExcelExportDto>
{
    private static readonly IReadOnlyList<string> ColumnNamesSpecification =
    [
        "Наименование",
        "Тип оборудования",
        "Параметры"
    ];

    public string SheetName => "Модели оборудования";
    public string FileName => "Модели оборудования.xlsx";

    public IReadOnlyList<string> ColumnNames => ColumnNamesSpecification;

    public IEnumerable<ColumnCellData> GetValuesToWrite(EquipmentSchemaRegistryExcelExportDto data)
    {
        yield return new ColumnCellData(ColumnNumber: 0, new StringExcelValueType(data.Name));
        yield return new ColumnCellData(ColumnNumber: 1, new StringExcelValueType(data.TypeName));
        yield return new ColumnCellData(ColumnNumber: 2, new StringExcelValueType(data.Parameters));
    }
}