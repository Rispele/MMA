using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Core.Services.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Services.Spreadsheets.ExportModels;

namespace Rooms.Core.Services.Spreadsheets.Specifications;

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