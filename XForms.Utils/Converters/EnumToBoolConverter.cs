using System;
using System.Globalization;
using Xamarin.Forms;

namespace XForms.Utils.Converters
{
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hz = (int)parameter;
            var hz2 = (int)value;
            return (int)parameter == (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
