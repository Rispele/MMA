using Rooms.Core.Services.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Services.Spreadsheets.Specifications;

public record ColumnSpecification<TData>(string Name, Func<TData, ISpreadsheetValueType> ValueExtractor);