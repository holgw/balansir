using System;
using Xamarin.Forms;

namespace BalansirApp.Controls.Converters
{
    public class IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
                return value.ToString();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int dec;
            if (int.TryParse(value as string, out dec))
                return dec;
            return 0;
        }
    }
}
