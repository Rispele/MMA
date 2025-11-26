using NPOI.SS.UserModel;

namespace Rooms.Core.Services.Spreadsheets.ExcelValueTypes;

public interface ISpreadsheetValueType
{
    void WriteToExcel(IRow row, int colNumber);
}