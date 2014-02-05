using System;
using System.Collections.Generic;
using System.IO;
using Granta.MaterialsWall.Models;
using OfficeOpenXml;

namespace Granta.MaterialsWall.DataAccess.Excel
{
    public sealed class ExcelImporter
    {
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

            var columnParser = new ColumnParser();
            var columns = columnParser.ParseColumns(worksheet);

            var rowParser = new RowParser(columns);
            var cards = new List<Card>();

            // skip the header row
            for (int rowIndex = 2; rowIndex <= worksheet.Dimension.End.Row; rowIndex++)
            {
                var card = rowParser.ParseRow(worksheet, rowIndex);

                if (card != null)
                {
                    cards.Add(card);
                }
            }

            return cards;
        }
    }
}
