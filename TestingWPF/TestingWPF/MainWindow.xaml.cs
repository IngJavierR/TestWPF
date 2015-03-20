using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using TestingWPF.Bindings;
using System.Collections.ObjectModel;
using Servicios;
using TestingWPF.Reportes;
using TestingWPF.Documentos;
using System.IO;
using TestingWPF.OpenXML;
using Microsoft.Win32;

namespace TestingWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 1;
        Person person = new Person("Javier", 29);
        ConsultasBD servicio = new ConsultasBD();
        IList<Customers> customers = new List<Customers>();

        public MainWindow()
        {
            InitializeComponent();
            PersonStack.DataContext = person;
            buttonName.Text = "Press Me!";
            this.birthdayButton.Click += birthdayButton_Click;
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ObservableCollection<Persona>()
            {
                new Persona("Oscar", "Alvarez", "Guerras", Persona.Deporte.Atletismo),
                new Persona("Jose", "Rodriguez", "Fernandez", Persona.Deporte.Futbol),
                new Persona("Jorge", "Elorza", "Blanco", Persona.Deporte.Baloncesto),
                new Persona("Noelia", "Gomez", "Souto", Persona.Deporte.Futbol)
            };
            comboBoxDeportes.ItemsSource = Enum.GetValues(typeof(Persona.Deporte));

            //customers = servicio.getCustomers();
            //tablaCustomer.ItemsSource = customers;

            var persons = servicio.getPersons();
            tablaCustomer.ItemsSource = persons;
        }

        void birthdayButton_Click(object sender, RoutedEventArgs e)
        {
            ++person.Age;
            person.Name += "!";
            //MessageBox.Show(string.Format("Happy Birthday {0}, age {1}", person.Name, person.Age), "BirthDay");
        }

        private void buttonTest_Click_1(object sender, RoutedEventArgs e)
        {
            switch (count)
            {
                case 1:
                    MessageBox.Show("Te amo");
                    break;
                case 2:
                    MessageBox.Show("muchisimo");
                    break;
                case 3:
                    MessageBox.Show("mi vida");
                    break;
                default:
                    MessageBox.Show(obtieneTextoFormateado("muchisimo!!"));
                    break;
            }
            count++;
        }

        public String obtieneTextoFormateado(String texto)
        {
            return texto.ToUpper();
        }

        private void buttonTest_Click_Consulta(object sender, RoutedEventArgs e)
        {

        }

        private void CommonHandlerButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Press Si, No");
        }

        private void tablaCustomer_SelectedCellsChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Persons selectedPerson = (Persons)tablaCustomer.SelectedItem;

            var window = new ReporteComun(selectedPerson);
            window.Show();
        }

        private void btn_CreateExcel_Click_1(object sender, RoutedEventArgs e)
        {
            var persons = servicio.getPersons();
            IPerson single = new IPerson();
            IList<IPerson> list = new List<IPerson>();
            foreach (var singlePerson in persons)
            {
                single = new IPerson();
                single.DateHour = singlePerson.DateHour;
                single.Fecha = singlePerson.Fecha;
                single.FirstName = singlePerson.FirstName;
                single.LastName = singlePerson.LastName;
                single.PersonID = singlePerson.PersonID;
                list.Add(single);
            }

            var mem = new MemoryStream();
            var export = new ExcelExportFromType<IPerson>(list, mem);
            export.Generate();

            return mem.ToArray();
        }

        private string GetFilePath()
        {
            var dialog = new SaveFileDialog
            {
                FileName = "Consulta",
                Filter = "Documentos de Excel | *.xlsx",
                DefaultExt = "xlsx"
            };
            var result = dialog.ShowDialog();
            if (!result.Value)
                return string.Empty;
            var archivo = dialog.FileName;
            if (string.IsNullOrWhiteSpace(archivo))
                archivo = Strings.ConsultaIncapacidades_NombreExcel;
            return archivo;
        }

    }
}
