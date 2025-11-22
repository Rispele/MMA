using FluentAssertions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Rooms.Tests.UnitTests.Spreadsheets;

public class SpreadsheetsAssertionHelper
{
    public static void AssertWorkbooks(
        string sheetName,
        XSSFWorkbook actualSpreadsheet,
        XSSFWorkbook expectedSpreadsheet)
    {
        var actualSheet = actualSpreadsheet.GetSheet(sheetName);
        var expectedSheet = expectedSpreadsheet.GetSheet(sheetName);

        actualSheet.Should().NotBeNull();
        expectedSheet.Should().NotBeNull();

        AssertSheets(actualSheet, expectedSheet);
    }

    private static void AssertSheets(ISheet actualSheet, ISheet expectedSheet)
    {
        foreach (var (expectedRow, rowIndex) in expectedSheet.AsEnumerable().Select((row, index) => (row, index)))
        {
            var actualRow = actualSheet.GetRow(rowIndex);

            actualRow.Should().NotBeNull();

            AssertRows(actualRow, expectedRow);
        }
    }

    private static void AssertRows(IRow actualRow, IRow expectedRow)
    {
        foreach (var (expectedCell, cellIndex) in expectedRow.AsEnumerable().Select((column, index) => (column, index: index)))
        {
            var actualCell = actualRow.GetCell(cellIndex);

            actualCell.Should().NotBeNull();

            actualCell.StringCellValue.Should().Be(expectedCell.StringCellValue);
        }
    }
}