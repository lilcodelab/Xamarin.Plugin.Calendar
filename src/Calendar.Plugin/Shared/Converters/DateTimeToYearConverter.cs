using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Converters
{
    public class DateTimeToYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
                return dt.Year;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int year)
                return new DateTime(year, 1, 1);

            return DateTime.MinValue;
        }
    }
}
