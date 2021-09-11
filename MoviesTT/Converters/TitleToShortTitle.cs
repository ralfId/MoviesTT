using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MoviesTT.Converters
{
    public class TitleToShortTitle : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var title = (string)value;
            if (title.Length > 19)
            {
                title = title.Substring(0, 15);
                title = title + "...";
                return title;
            }
            return title;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
