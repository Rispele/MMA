using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Core.Services.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Services.Spreadsheets.ExportModels;

namespace Rooms.Core.Services.Spreadsheets.Specifications;

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