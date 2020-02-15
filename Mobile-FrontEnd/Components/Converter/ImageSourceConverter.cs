using System;
using Xamarin.Forms;

namespace MobileFrontEnd.Components.Converter
{
    public class ImageSourceConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string source = value as string;
            if (source == null || string.IsNullOrWhiteSpace(source))
                return null;

            return ImageSource.FromResource(source);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}