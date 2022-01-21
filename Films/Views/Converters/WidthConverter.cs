using System;
using System.Globalization;
using System.Windows.Data;

namespace Films.Activitis.Converters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int width = System.Convert.ToInt32(value);
            return width > 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}