using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TestingWPF.OpenXML
{

    public static class OpenXMLExtensions
    {
        private const string WordNamespace = "{http://schemas.openxmlformats.org/wordprocessingml/2006/main}";

        /// <summary>
        /// Escribe dentro del OpenXmlPart una cadena xml
        /// </summary>
        /// <param name="part"></param>
        /// <param name="xmlStringPart">Cadena OpenXml a escribir</param>
        public static void FeedString(this OpenXmlPart part, String xmlStringPart)
        {
            using (var stream = part.GetStream())
            {
                var buf = (new UTF8Encoding()).GetBytes(xmlStringPart);
                stream.Write(buf, 0, buf.Length);
            }
        }

        /// <summary>
        /// Elimina el background gris de las partes no editables del documento en el contenedor dado
        /// </summary>
        /// <param name="document">Contenedor</param>
        public static void RemovePermissionMarks(this XContainer document)
        {
            //Se eliminan los permisos de inicio y final
            document.Descendants(string.Format("{0}permStart", WordNamespace)).Concat(
                document.Descendants(string.Format("{0}permEnd", WordNamespace))).Remove();
        }

        public static void RemoveBookMarks(this XContainer document)
        {
            //Se eliminan los permisos de inicio y final
            document.Descendants(string.Format("{0}bookmarkStart", WordNamespace)).Concat(
                document.Descendants(string.Format("{0}bookmarkEnd", WordNamespace))).Remove();
        }

        /// <summary>
        /// Elimina el background gris de las partes no editables del documento en el contenedor dado
        /// </summary>
        /// <param name="doc">Contenedor</param>
        public static void RemoveBookMarks(this WordprocessingDocument doc)
        {
            //Se eliminan los permisos de inicio y final
            doc.MainDocumentPart.Document.Descendants<BookmarkStart>().Cast<OpenXmlElement>().Concat(
                doc.MainDocumentPart.Document.Descendants<BookmarkEnd>()).ForEachDo(e => e.Remove());
        }

        /// <summary>
        /// Se crea la parte de estilos del documento si se indica una cadena xml se escribe en la parte de estilos
        /// </summary>
        /// <param name="mainPart">Sección principal del documento</param>
        /// <param name="styles">Cadena Xml que define los estilos</param>
        public static void CreateStylePart(this MainDocumentPart mainPart, string styles = null)
        {
            if (mainPart.StyleDefinitionsPart != null)
                mainPart.DeletePart(mainPart.StyleDefinitionsPart);

            //Se llena la parte de estilos
            var stylePart = mainPart.AddNewPart<StyleDefinitionsPart>();
            if (!string.IsNullOrEmpty(styles))
                stylePart.FeedString(styles);
            mainPart.StyleDefinitionsPart.Styles.Save();
        }

        /// <summary>
        /// Se crea la parte de contenido del documento si se indica una cadena xml se escribe en la parte de contenido
        /// </summary>
        /// <param name="mainPart">Sección principal del documento</param>
        /// <param name="documentPart">Cadena Xml que define el contenido</param>
        public static void CreateDocumentPart(this MainDocumentPart mainPart, string documentPart)
        {
            //Se transforma el cuerpo del documento a XDocument
            if (string.IsNullOrEmpty(documentPart))
                return;
            var document = XDocument.Parse(documentPart);
            var contenidoDocumento = document.ToString();
            mainPart.FeedString(contenidoDocumento);
        }
        
    }
}