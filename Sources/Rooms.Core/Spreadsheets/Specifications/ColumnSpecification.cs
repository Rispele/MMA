using Rooms.Core.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Spreadsheets.Specifications;

public record ColumnSpecification<TData>(string Name, Func<TData, ISpreadsheetValueType> ValueExtractor);