using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TestingWPF.OpenXML
{

    public static class ContentControlExtensions
    {
        /// <summary>
        /// Busca una cadena dentro de los content controls del documento y remplaza su contenido en un solo bloque de texto
        /// </summary>
        /// <param name="doc">Documento que contiene el content control</param>
        /// <param name="text">Texto a reemplazar</param>
        /// <param name="replaceText">Texto nuevo</param>
        /// <returns>Documento modificado</returns>
        public static WordprocessingDocument Replace(this WordprocessingDocument doc, String text, String replaceText)
        {
            var document = doc.MainDocumentPart.Document;
            var contentControls = document.SelectMany(r =>
                                                      r.Descendants<SdtContentRun>()
                                                          .Cast<OpenXmlElement>()
                                                          .Concat(r.Descendants<SdtContentBlock>()));
            var findedControls = contentControls.Where(cc => cc.InnerText == text).ToList();
            findedControls.ForEach(
                cc => ReplaceContent(replaceText, cc));
            return doc;
        }

        /// <summary>
        /// Busca una cadena dentro de los content controls del documento y remplaza su contenido por una serie de renglones
        /// </summary>
        /// <param name="doc">Documento que contiene el content control</param>
        /// <param name="text">Texto a reemplazar</param>
        /// <param name="lines">Líneas con contenido nuevo</param>
        /// <returns>Documento modificado</returns>
        public static WordprocessingDocument Replace(this WordprocessingDocument doc, String text, IEnumerable<string> lines)
        {
            var lineList = lines.ToList();
            if (!lineList.Any())
                return doc.Replace(text, string.Empty);
            var replaceContent = GeneraRenglon(lineList);
            var document = doc.MainDocumentPart.Document;
            var contentControls = document.SelectMany(r =>
                                                      r.Descendants<SdtBlock>()
                                                          .Cast<OpenXmlElement>()
                                                          .Concat(r.Descendants<SdtRun>()));
            var findedControls = contentControls.Where(cc => cc.InnerText == text).ToList();
            findedControls.ForEach(
                cc =>
                    {
                        var block = new SdtBlock();
                        var blockContent = new SdtContentBlock();
                        var runProps = GetRunProperties(cc);
                        block.Append(new SdtProperties(runProps));
                        GenerateParagraphsForContent(runProps, blockContent, replaceContent);
                        block.Append(blockContent);
                        cc.InsertAfterSelf(block);
                        cc.Remove();
                    });
            return doc;
        }

        private static void GenerateParagraphsForContent(RunProperties runProps, SdtContentBlock blockContent, IEnumerable<Text> replaceContent)
        {
            replaceContent.ForEachDo(d =>
                                         {
                                             var run = new Run();
                                             run.Append((OpenXmlElement)runProps.Clone());
                                             run.Append(d);
                                             var par = new Paragraph(run);
                                             blockContent.Append(par);
                                         });
        }

        private static RunProperties GetRunProperties(OpenXmlElement contentControl)
        {
            var sdtProps = contentControl.Descendants<SdtProperties>().FirstOrDefault();
            if (sdtProps == null)
                return new RunProperties();
            var runProps = sdtProps.Descendants<RunProperties>().FirstOrDefault();
            if (runProps== null)
                return new RunProperties();
            return (RunProperties) runProps.Clone();
        }

        private static  IEnumerable<Text> GeneraRenglon(IEnumerable<string> diagnostico)
        {
            return diagnostico.Select(d => new Text(d));
        }

        /// <summary>
        /// Reemplaza el contenido de un content control
        /// </summary>
        /// <param name="replaceText">Texto por el que será reemplazado</param>
        /// <param name="cc">Content control</param>
        private static void ReplaceContent(string replaceText, OpenXmlElement cc)
        {
            var firstRun = cc.Descendants<Run>().FirstOrDefault();
            if (firstRun == null)
                CreateRun(replaceText, cc);
            else
            {
                ReplaceText(replaceText, firstRun);
                cc.Descendants<Run>().Where(d => d != firstRun)
                    .ToList().ForEach(e => e.Remove());
            }
        }

        private static void ReplaceText(string replaceText, Run firstRun)
        {
            var firstText = firstRun.Descendants<Text>().FirstOrDefault();
            if (firstText != null)
                firstText.Text = replaceText;
            else
                CreateText(replaceText, firstRun);
        }

        private static void CreateText(string replaceText, Run firstRun)
        {
            var firstText = new Text(replaceText);
            firstRun.FirstChild.InsertAfterSelf(firstText);
        }

        private static void CreateRun(string replaceText, OpenXmlElement cc)
        {
            var firstRun = new Run(new Text(replaceText));
            cc.FirstChild.InsertAfterSelf(firstRun);
        }
    }
}