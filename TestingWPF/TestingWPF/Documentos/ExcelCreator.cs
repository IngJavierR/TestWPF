using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Servicios;

namespace TestingWPF.Documentos
{
    class ExcelCreator
    {
        public void InitExcel(IList<Persons> persons)
        {
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open("C:\\Users\\Ing.Javier\\Documents\\ExcelFile.xlsx", false))
            {
                WorkbookPart workBookPart = spreadSheetDocument.WorkbookPart;
                WorksheetPart workSheetPart = workBookPart.WorksheetParts.First();
                SheetData sheeData = workSheetPart.Worksheet.GetFirstChild<SheetData>();
                int index = 2;
                foreach (var person in persons)
                {
                    Row contentRow = CreateContentRow(index, person);
                    index++;
                    sheeData.AppendChild(contentRow);
                }
                
            }
        }

        private Row CreateContentRow(int index, Persons persona)
        {
            int letter = 65;
            string colLetter = ((char)letter).ToString();
            Row row = new Row();
            row.RowIndex = (UInt32)index;

            Cell firstCell = CreateTextCell(colLetter, persona.FirstName, index);
            row.AppendChild(firstCell);
            letter++;

            firstCell = CreateTextCell(colLetter, persona.LastName, index);
            row.AppendChild(firstCell);
            letter++;

            firstCell = CreateTextCell(colLetter, persona.PersonID+"", index);
            row.AppendChild(firstCell);
            letter++;

            firstCell = CreateTextCell(colLetter, persona.Fecha.ToString(), index);
            row.AppendChild(firstCell);
            letter++;

            firstCell = CreateTextCell(colLetter, persona.DateHour, index);
            row.AppendChild(firstCell);

            return row;
        }

        private Cell CreateTextCell(string header, string text, int index)
        {
            Cell cell = new Cell() 
                { 
                    DataType = CellValues.String, 
                    CellReference = (header + index),
                    CellValue = new CellValue(text),
                };


            return null;
        }

    }
}
