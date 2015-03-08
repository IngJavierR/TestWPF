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

namespace TestingWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 1;
        public MainWindow()
        {
            InitializeComponent();
            buttonName.Text = "Press Me!";
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

        
    }
}
