using Rooms.Core.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Spreadsheets.Abstractions;

public record ColumnCellData(int ColumnNumber, ISpreadsheetValueType Value);