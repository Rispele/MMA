using Commons;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using Rooms.Core.Spreadsheets.Abstractions;

namespace Rooms.Infrastructure.Spreadsheets;

public static class ExcelWriter
{
    public static void Write<TSpecification, TData>(ISheet worksheet, TData[] data)
        where TSpecification : struct, ISpreadsheetWriterSpecification<TData>
    {
        var style = worksheet.Workbook.CreateCellStyle();
        style.FillBackgroundColor = HSSFColor.Yellow.Index;
        style.FillPattern = FillPattern.SolidForeground;

        var specification = new TSpecification();

        var nextRow = FillHeader(specification, worksheet, rowNumber: 0);
        FillBody(specification, worksheet, data, nextRow);
    }

    private static int FillHeader<TData>(
        ISpreadsheetWriterSpecification<TData> specification,
        ISheet worksheet,
        int rowNumber)
    {
        var row = worksheet.CreateRow(rowNumber);

        specification.ColumnNames.ForEach((name, index) => row.CreateCell(index).SetCellValue(name));

        return NextRow(rowNumber);
    }

    private static void FillBody<TData>(
        ISpreadsheetWriterSpecification<TData> specification,
        ISheet worksheet,
        TData[] dataToExport,
        int nextRow)
    {
        _ = dataToExport.Aggregate(nextRow, func: (index, data) => FillDataRow(specification, worksheet, data, index));
    }

    private static int FillDataRow<TData>(
        ISpreadsheetWriterSpecification<TData> writerSpecification,
        ISheet worksheet,
        TData data,
        int rowNumber)
    {
        var row = worksheet.CreateRow(rowNumber);

        writerSpecification
            .GetValuesToWrite(data)
            .ForEach(mapped => mapped.Value.WriteToExcel(row, mapped.ColumnNumber));

        return NextRow(rowNumber);
    }

    private static int NextRow(int rowNumber)
    {
        return rowNumber + 1;
    }
}