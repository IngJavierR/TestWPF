using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace TestingWPF.Resources
{
    [ValueConversion(typeof(String), typeof(DateTime))]
    public class ConvertStringToHour : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string timeDate = value.ToString();
            DateTime time = DateTime.ParseExact(timeDate, "hh:mm tt", null);
            return time;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
