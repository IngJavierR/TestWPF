using System;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TestingWPF.OpenXML
{
    public class ExcelDocumentCreatorHelper
    {
        protected void SetCellValue(Cell cell,object value,SharedStringTable sharedStringTable)
        {
            if (value is int || value is long || value is decimal || value is float)
            {
                cell.DataType = CellValues.Number;
                cell.CellValue = new CellValue(value.ToString());
                return;
            }

            cell.DataType = CellValues.SharedString;
            var sharedString = sharedStringTable.Descendants<SharedStringItem>().FirstOrDefault(si => si.Text.Text == value.ToString());
            if (sharedString != null)
            {
                var existingStringvalue = sharedStringTable.ToList().IndexOf(sharedString);
                cell.CellValue = new CellValue(existingStringvalue.ToString(CultureInfo.InvariantCulture));
                return;
            }
            cell.CellValue = new CellValue(AddSharedString(sharedStringTable, value.ToString()));
        }

        private string AddSharedString(SharedStringTable stringTable, String text)
        {
            var sharedStringItem1 = new SharedStringItem();
            var text1 = new Text {Text = text};
            sharedStringItem1.Append(text1);
            stringTable.Append(sharedStringItem1);
            return stringTable.Descendants<SharedStringItem>()
                .ToList()
                .IndexOf(sharedStringItem1)
                .ToString(CultureInfo.InvariantCulture);
        }

        public Cell CreateCell(object value, SharedStringTable sharedStringTable, string nextCell, uint styleIndex)
        {
            var cell = new Cell
                           {
                               CellReference = nextCell,
                               StyleIndex = styleIndex
                           };

            SetCellValue(cell, value, sharedStringTable);
            return cell;
        }
    }
}