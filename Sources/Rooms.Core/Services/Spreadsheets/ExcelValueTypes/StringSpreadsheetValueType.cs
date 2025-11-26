using NPOI.SS.UserModel;

namespace Rooms.Core.Services.Spreadsheets.ExcelValueTypes;

public class StringSpreadsheetValueType(string? value) : ISpreadsheetValueType
{
    private string? Value { get; } = value;

    public void WriteToExcel(IRow row, int colNumber)
    {
        row.CreateCell(colNumber).SetCellValue(Value);
    }
}