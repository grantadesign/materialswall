using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Granta.MaterialsWall.Models;
using OfficeOpenXml;

namespace Granta.MaterialsWall.DataAccess.Excel
{
    public interface IExcelImporter
    {
        IEnumerable<Card> ParseData(string filename);
    }

    public sealed class ExcelImporter : IExcelImporter
    {
        private readonly IColumnParser columnParser;
        private readonly IRowParser rowParser;

        public ExcelImporter(IColumnParser columnParser, IRowParser rowParser)
        {
            if (columnParser == null)
            {
                throw new ArgumentNullException("columnParser");
            }

            if (rowParser == null)
            {
                throw new ArgumentNullException("rowParser");
            }
            
            this.columnParser = columnParser;
            this.rowParser = rowParser;
        }

        public IEnumerable<Card> ParseData(string filename)
        {
            var excelPackage = new ExcelPackage(new FileInfo(filename));
            var workbook = excelPackage.Workbook;

            if (workbook.Worksheets.Count != 1)
            {
                string message = string.Format("Incorrect number of worksheets (expected 1, found {0})", workbook.Worksheets.Count);
                throw new ArgumentException(message);
            }

            var worksheet = workbook.Worksheets[1];
            var columns = columnParser.ParseColumns(worksheet).ToList();
            var cards = new List<Card>();

            // skip the header row
            for (int rowIndex = 2; rowIndex <= worksheet.Dimension.End.Row; rowIndex++)
            {
                var card = rowParser.ParseRow(columns, worksheet, rowIndex);
                cards.Add(card);
            }

            return cards.Where(c => c != null);
        }
    }
}
