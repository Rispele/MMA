using Rooms.Core.Services.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Services.Spreadsheets.Abstractions;

public record ColumnCellData(int ColumnNumber, ISpreadsheetValueType Value);