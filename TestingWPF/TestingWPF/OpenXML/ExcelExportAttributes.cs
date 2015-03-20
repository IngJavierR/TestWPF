using System;
using System.Globalization;

namespace TestingWPF.OpenXML
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ExcelExportInfoAttribute : Attribute
    {
        public String SheetTitle { get; set; }

        public ExcelExportInfoAttribute(string sheetTitle)
        {
            SheetTitle = sheetTitle;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ExcelExportColumnAttribute : Attribute
    {
        public enum TextModifierEnum
        {
            None = 0,
            Upper = 1,
            Lower = 2,
            TitleCase = 3
        }

        public TextModifierEnum Modifier { get; set; }

        public String ColumnHeader { get; set; }

        public ExcelExportColumnAttribute(string columnHeader)
        {
            ColumnHeader = columnHeader;
        }

        public String Format { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ExelExportIgnoreAttribute : Attribute
    {
    }
}