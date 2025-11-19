using NPOI.SS.UserModel;

namespace Rooms.Core.Spreadsheets.ExcelValueTypes;

public class StringSpreadsheetValueType(string? value) : ISpreadsheetValueType
{
    private string? Value { get; } = value;

    public void WriteToExcel(IRow row, int colNumber)
    {
        row.CreateCell(colNumber).SetCellValue(Value);
    }
}