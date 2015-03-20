using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using TestingWPF.OpenXML;

namespace TestingWPF.Bindings
{
    [DataContract]
    [ExcelExportInfo("Consulta de incapacidades")]
    class IPerson
    {
        [DataMember]
        [ExcelExportColumn("PersonaId")]
        public int? PersonID { get; set; }

        [DataMember]
        [ExcelExportColumn("Apellido")]
        public string LastName { get; set; }

        [DataMember]
        [ExcelExportColumn("Nombre")]
        public string FirstName { get; set; }

        [DataMember]
        [ExcelExportColumn("Hora")]
        public string DateHour { get; set; }

        [DataMember]
        [ExcelExportColumn("Fecha")]
        public DateTime? Fecha { get; set; }
    }
}
