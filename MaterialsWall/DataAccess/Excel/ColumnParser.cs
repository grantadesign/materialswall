using System.Collections.Generic;
using OfficeOpenXml;

namespace Granta.MaterialsWall.DataAccess.Excel
{
    public sealed class ColumnParser
    {
        public IDictionary<string, Column> ParseColumns(ExcelWorksheet worksheet)
        {
            var columns = new Dictionary<string, Column>();

            for (int columnIndex = 1; columnIndex <= worksheet.Dimension.End.Column; columnIndex++)
            {
                string columnName = worksheet.Cells[1, columnIndex].GetValue<string>() ?? string.Empty;
                var column = new Column(columnName, columnIndex);
                columns.Add(columnName, column);
            }

            return columns;
        }
    }
}
