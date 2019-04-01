using System;
using System.Globalization;
using Xamarin.Forms;

namespace XForms.Utils.Converters
{
    public class NullableDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return (double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string stringValue = value as string;
            if (string.IsNullOrEmpty(stringValue))
                return null;

            if (double.TryParse(stringValue, out double valueToConvertTo))
            {
                if (valueToConvertTo == 0)
                {
                    return null;
                }

                return valueToConvertTo;
            }

            return null;
        }
    }
}
