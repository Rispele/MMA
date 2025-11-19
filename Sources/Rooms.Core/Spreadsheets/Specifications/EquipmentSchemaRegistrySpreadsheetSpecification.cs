using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Spreadsheets.ExportModels;

namespace Rooms.Core.Spreadsheets.Specifications;

public struct EquipmentSchemaRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<EquipmentSchemaRegistrySpreadsheetExportDto>
{
    private static readonly IReadOnlyList<ColumnSpecification<EquipmentSchemaRegistrySpreadsheetExportDto>> Specifications =
    [
        new(Name: "Наименование", data => new StringSpreadsheetValueType(data.Name)),
        new(Name: "Тип оборудования", data => new StringSpreadsheetValueType(data.TypeName)),
        new(Name: "Параметры", data => new StringSpreadsheetValueType(data.Parameters)),
    ];

    public string SheetName => "Модели оборудования";
    public string FileName => "Модели оборудования.xlsx";

    public IReadOnlyList<ColumnSpecification<EquipmentSchemaRegistrySpreadsheetExportDto>> ColumnSpecifications => Specifications;
}