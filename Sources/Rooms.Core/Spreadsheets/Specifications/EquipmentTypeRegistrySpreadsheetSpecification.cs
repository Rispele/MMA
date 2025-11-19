using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Spreadsheets.ExportModels;

namespace Rooms.Core.Spreadsheets.Specifications;

public struct EquipmentTypeRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<EquipmentTypeRegistrySpreadsheetExportDto>
{
    private static readonly IReadOnlyList<ColumnSpecification<EquipmentTypeRegistrySpreadsheetExportDto>> Specifications =
    [
        new(Name: "Наименование", data => new StringSpreadsheetValueType(data.Name)),
    ];
    public string SheetName => "Типы оборудования";
    public string FileName => "Типы оборудования.xlsx";

    public IReadOnlyList<ColumnSpecification<EquipmentTypeRegistrySpreadsheetExportDto>> ColumnSpecifications => Specifications;
}