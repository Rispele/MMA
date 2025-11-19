using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Spreadsheets.Specifications;

public struct EquipmentTypeRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<EquipmentTypeRegistryExcelExportDto>
{
    private static readonly IReadOnlyList<ColumnSpecification<EquipmentTypeRegistryExcelExportDto>> Specifications =
    [
        new(Name: "Наименование", data => new StringSpreadsheetValueType(data.Name)),
    ];
    public string SheetName => "Типы оборудования";
    public string FileName => "Типы оборудования.xlsx";

    public IReadOnlyList<ColumnSpecification<EquipmentTypeRegistryExcelExportDto>> ColumnSpecifications => Specifications;
}