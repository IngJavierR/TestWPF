using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TestingWPF.OpenXML
{
    public class ExcelExportFromType<T>
    {
        private readonly IEnumerable<T> _source;
        private readonly string _filename;
        private readonly MemoryStream _stream;
        private readonly List<PropertyInfo> _propertyList;
        private readonly SpreadsheetDocument _spreadSheet;
        private WorksheetPart _workSheetPart;
        private SharedStringTablePart _sharedStringsPart;
        private SheetData _sheetData;
        private readonly ExcelDocumentCreatorHelper _documentHelper = new ExcelDocumentCreatorHelper();

        public ExcelExportFromType(IEnumerable<T> source, MemoryStream stream)
        {
            _source = source;
            _stream = stream;
            _propertyList = ExtractProperties();
            _spreadSheet = SpreadsheetDocument.Create(_stream, SpreadsheetDocumentType.Workbook);
            InitDocument();
        }

        public ExcelExportFromType(IEnumerable<T> source, String filename)
        {
            _source = source;
            _filename = filename;
            _propertyList = ExtractProperties();
            _spreadSheet = SpreadsheetDocument.Create(_filename, SpreadsheetDocumentType.Workbook);
            InitDocument();
        }

        public void Generate()
        {
            CreateDocumentContent();
        }

        private void CreateDocumentContent()
        {
            FillRows(_source);
            Save();
        }

        private void InitDocument()
        {
            _spreadSheet.AddWorkbookPart();
            _spreadSheet.WorkbookPart.Workbook = new Workbook();
            _workSheetPart = _spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
            _workSheetPart.Worksheet = new Worksheet();
            _sharedStringsPart = _spreadSheet.WorkbookPart.AddNewPart<SharedStringTablePart>();
            CreateColumnsAndHeaders();
        }

        private void CreateColumnsAndHeaders()
        {
            var propertyDict = GetMaxLenghtPropertyDictionary(_propertyList, _source);
            var columns1 = ConfigureColumns(propertyDict);
            _workSheetPart.Worksheet.Append(columns1);
            _spreadSheet.WorkbookPart.CreateStyleSheet();
            var sharedStringTable = new SharedStringTable();
            _sheetData = new SheetData();
            AppendHeaders(_sheetData, sharedStringTable, _propertyList);
            SetSheetata();
            _sharedStringsPart.SharedStringTable = sharedStringTable;
        }

        private void Save()
        {
            _workSheetPart.Worksheet.Append(_sheetData);
            _spreadSheet.WorkbookPart.Workbook.Save();
            _spreadSheet.Dispose();
        }

        private void SetSheetata()
        {
            _spreadSheet.WorkbookPart.WorksheetParts.First().Worksheet.Save();
            _spreadSheet.WorkbookPart.Workbook.AppendChild(new Sheets());
            _spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                .AppendChild(new Sheet
                                 {
                                     Id = WorkSheetPartId,
                                     SheetId = 1,
                                     Name = GetSheetTitle()
                                 });
        }

        private string WorkSheetPartId
        {
            get { return _spreadSheet.WorkbookPart.GetIdOfPart(_workSheetPart); }
        }

        private void FillRows(IEnumerable<T> source)
        {
            UInt32Value counter = 2;
            foreach (var element in source)
            {
                AppendRow(_sheetData, _sharedStringsPart.SharedStringTable, element, _propertyList, counter);
                counter++;
            }
        }

        private Dictionary<PropertyInfo,double> GetMaxLenghtPropertyDictionary(IEnumerable<PropertyInfo> props, IEnumerable<T> source)
        {
            return props.ToDictionary(p => p, 
                p =>
                    {
                        var maxlenght = source.Max(e =>
                                                       {
                                                           var value = p.GetValue(e, null);
                                                           return value == null ? 0 : value.ToString().Length;
                                                       });
                        var attr = p.GetCustomAttributes(true).OfType<ExcelExportColumnAttribute>().FirstOrDefault();
                        if (attr != null && !string.IsNullOrWhiteSpace(attr.ColumnHeader) && attr.ColumnHeader.Length > maxlenght)
                            return attr.ColumnHeader.Length * 1.2;
                        return p.Name.Length > maxlenght ? p.Name.Length * 1.2 : maxlenght * 1.2;
                    });
        }

        private List<PropertyInfo> ExtractProperties()
        {
            return typeof (T).GetProperties()
                .Where(p => !p.GetCustomAttributes(true).OfType<ExelExportIgnoreAttribute>().Any())
                .ToList();
        }

        private string GetSheetTitle()
        {
            var attr = typeof (T).GetCustomAttributes(true).OfType<ExcelExportInfoAttribute>().FirstOrDefault();
            return attr != null ? attr.SheetTitle : typeof (T).Name;
        }

        private void AppendRow(SheetData sheetData, SharedStringTable sharedStringTable, T element, IEnumerable<PropertyInfo> propertyList, UInt32Value counter)
        {
            var firstChar = 65;
            var row = new Row {RowIndex = counter};
            foreach (var prop in propertyList)
            {
                var attr = prop.GetCustomAttributes(true).OfType<ExcelExportColumnAttribute>().FirstOrDefault();
                var nextCell = string.Format("{0}{1}", Convert.ToChar(firstChar), counter);
                var value = prop.GetValue(element, null);;
                value = SetFormat(value, attr);
                row.Append(CreateCell(value, sharedStringTable, nextCell,true));
                firstChar++;
            }
            sheetData.Append(row);
        }

        public static string Modify(ExcelExportColumnAttribute.TextModifierEnum modifier, string value)
        {
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            switch (modifier)
            {
                case ExcelExportColumnAttribute.TextModifierEnum.Upper:
                    return value.ToUpper();
                case ExcelExportColumnAttribute.TextModifierEnum.Lower:
                    return value.ToLower();
                case ExcelExportColumnAttribute.TextModifierEnum.TitleCase:
                    return textInfo.ToTitleCase(value);
                default:
                    return value;
            }
        }

        private static object SetFormat(object value, ExcelExportColumnAttribute attr)
        {
            if (value == null)
                return string.Empty;
            if (attr == null)
                return value;
            if (!string.IsNullOrEmpty(attr.Format))
            {
                var formato = "{0:" + attr.Format + "}";
                value = string.Format(formato, value);
            }
            return Modify(attr.Modifier,value.ToString());
        }

        private void AppendHeaders(SheetData sheetData, SharedStringTable sharedStringTable, IEnumerable<PropertyInfo> propertyList)
        {
            var firstChar = 65;
            var row = new Row { RowIndex = 1 };
            foreach (var prop in propertyList)
            {
                var nextCell = string.Format("{0}{1}", Convert.ToChar(firstChar), 1);
                var valueAttr = prop.GetCustomAttributes(true).OfType<ExcelExportColumnAttribute>().FirstOrDefault();
                string value = valueAttr == null ? prop.Name : valueAttr.ColumnHeader;
                row.Append(CreateCell(value, sharedStringTable, nextCell));
                firstChar++;
            }
            sheetData.Append(row);
        }

        private Columns ConfigureColumns(IEnumerable<KeyValuePair<PropertyInfo, double>> propertyList)
        {
            var columns1 = new Columns();
            var i = 1;
            foreach (var keyValuePair in propertyList)
            {
                var column1 = new Column { Min = (uint)i, Max = (uint)i, Width = keyValuePair.Value, BestFit = true, CustomWidth = true };
                columns1.Append(column1);
                i++;
            }
            return columns1;
        }

        private Cell CreateCell(object value, SharedStringTable sharedStringTable, string nextCell, bool isHeader = false)
        {
            return _documentHelper.CreateCell(value, sharedStringTable, nextCell, isHeader ? 0U : 1U);
        }
    }


}
