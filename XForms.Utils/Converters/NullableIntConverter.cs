using System;
using System.Globalization;
using Xamarin.Forms;

namespace XForms.Utils.Converters
{
    public class NullableIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string stringValue = value as string;
            if (string.IsNullOrEmpty(stringValue))
                return null;

            if (int.TryParse(stringValue, out int valueToConvertTo))
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
