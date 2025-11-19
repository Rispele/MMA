using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Spreadsheets.Specifications;

public struct EquipmentTypeRegistrySpreadsheetWriterSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<EquipmentTypeRegistryExcelExportDto>
{
    private static readonly IReadOnlyList<string> ColumnNamesSpecification =
    [
        "Наименование",
    ];

    public string SheetName => "Типы оборудования";
    public string FileName => "Типы оборудования.xlsx";
    
    public IReadOnlyList<string> ColumnNames => ColumnNamesSpecification;

    public IEnumerable<ColumnCellData> GetValuesToWrite(EquipmentTypeRegistryExcelExportDto data)
    {
        yield return new ColumnCellData(ColumnNumber: 0, new StringExcelValueType(data.Name));
    }
}