using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TestingWPF
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Creating a Global culture specific to our application.
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("es-MX");

            // Creating the DateTime Information specific to our application.
            System.Globalization.DateTimeFormatInfo dateTimeInfo = new System.Globalization.DateTimeFormatInfo();

            // Defining various date and time formats.
            dateTimeInfo.DateSeparator = "/";
            dateTimeInfo.LongDatePattern = "dd/MM/yyyy";
            dateTimeInfo.ShortDatePattern = "dd/MM/yyyy";
            dateTimeInfo.LongTimePattern = "hh:mm:ss tt";
            dateTimeInfo.ShortTimePattern = "hh:mm tt";

            // Setting application wide date time format.
            cultureInfo.DateTimeFormat = dateTimeInfo;

            // Assigning our custom Culture to the application.                        
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
            new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}
