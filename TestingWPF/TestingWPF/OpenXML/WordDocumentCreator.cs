using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TestingWPF.OpenXML
{
    public class WordDocumentGenerator
    {
        public byte[] CreateNew(string document,string styles, Action<WordprocessingDocument> processDocument = null)
        {
            using (var mem = new MemoryStream())
            {
                using (var doc = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document))
                {
                    var mainPart = doc.AddMainDocumentPart();
                    mainPart.CreateStylePart(styles);
                    mainPart.CreateDocumentPart(document);
                    ProcessAndSave(processDocument, mainPart, doc);
                }
                return mem.ToArray();
            }
        }

        private static void ProcessAndSave(Action<WordprocessingDocument> processDocument, MainDocumentPart mainPart, WordprocessingDocument doc)
        {
            if (processDocument != null)
                processDocument(doc);
            mainPart.Document.Save();
        }

        public byte[] CreateNew(byte[] documentBytes, Action<WordprocessingDocument> processDocument = null)
        {
            using (var mem = new MemoryStream())
            {
                mem.Write(documentBytes,0,documentBytes.Length);
                using (var doc = WordprocessingDocument.Open(mem,true))
                {
                    var mainPart = doc.MainDocumentPart;
                    ProcessAndSave(processDocument, mainPart, doc);
                }
                return mem.ToArray();
            }
        }
    }

    public class BookMarkRowSection
    {
        private IEnumerable<OpenXmlElement> TableRows
        {
            get
            {
                var rows = new List<OpenXmlElement>();
                OpenXmlElement table = _start.Ancestors<TableRow>().FirstOrDefault();
                if (table != null)
                    rows.Add(table);
                while (table != null && !table.Descendants().Contains(_end))
                {
                    var elem = table.NextSibling<TableRow>();
                    rows.Add(table);
                    table = elem;
                }
                return rows;
            }
        }

        public IEnumerable<String> Values
        {
            get
            {
                return ContentControls.Select(cc => cc.InnerText);
            }
        }

        public bool HasContent
        {
            get { return !ContentControls.All(
                cc => cc.InnerText.StartsWith("@") || string.IsNullOrWhiteSpace(cc.InnerText)); }
        }

        public IEnumerable<OpenXmlElement> ContentControls
        {
            get
            {
                return TableRows.SelectMany(r =>
                    r.Descendants<SdtContentRun>()
                    .Cast<OpenXmlElement>()
                    .Concat(r.Descendants<SdtContentBlock>()));
            }
        }

        public BookMarkRowSection(BookmarkStart start,BookmarkEnd end)
        {
            _start = start;
            _end = end;
            SectionName = _start.Name.Value;
            SectionIndex = int.Parse(_start.Id.Value);
        }

        public String SectionName { get; set; }

        private readonly BookmarkStart _start;

        private readonly BookmarkEnd _end;

        public void RemoveSection()
        {
            OpenXmlElement table = _start.Ancestors<TableRow>().FirstOrDefault();
            while (table != null && table is TableRow && !table.Descendants().Contains(_end))
            {
                var elem = table.NextSibling();
                table.Remove();
                table = elem;
            }
            _start.Remove();
            _end.Remove();
        }

        public int SectionIndex { get; set; }

        public static IEnumerable<BookMarkRowSection> GetSections(WordprocessingDocument doc)
        {
            var document = doc.MainDocumentPart.Document;
            var bookmarkstarts = document.Descendants<BookmarkStart>()
                .Where(b => !b.Name.Value.StartsWith("_"));
            var bookmarkEnds = document.Descendants<BookmarkEnd>();
            return bookmarkstarts.Select(
                bs =>
                new BookMarkRowSection(bs, bookmarkEnds.SingleOrDefault(be => be.Id.Value == bs.Id.Value)));
        }
    }
}
