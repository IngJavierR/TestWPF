using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TestingWPF.Bindings
{
    class Persona : INotifyPropertyChanged
    {
        public Persona() { }

        public Persona(string nombre, string apellido1, string apellido2, Deporte deporte)
        {
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.deporte_practico = deporte;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyChanges(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public enum Deporte
        {
            Futbol,
            Baloncesto,
            Atletismo,
            Balonmano,
            Golf
        }

         string nombre;

         public string Nombre
         {
             get { return nombre; }
             set 
             {
                 if (this.nombre != value)
                 {
                     this.nombre = value;
                     NotifyChanges("Nombre");
                 }
                 

             }
         }
         string apellido1;

         public string Apellido1
         {
             get { return apellido1; }
             set 
             {
                 if (this.apellido1 != value)
                 {
                     this.apellido1 = value;
                     NotifyChanges("Apellido1");
                 }
             }
         }
         string apellido2;

         public string Apellido2
         {
             get { return apellido2; }
             set 
             {
                 if (this.apellido2 != value)
                 {
                     this.apellido2 = value;
                     NotifyChanges("Apellido2");
                 }
             }
         }
         Deporte deporte_practico;

         private Deporte Deporte_practico
         {
             get { return deporte_practico; }
             set 
             {
                 if (this.deporte_practico != value)
                 {
                     this.deporte_practico = value;
                     NotifyChanges("Deporte_practico");
                 }
             }
         }    
    }
}
