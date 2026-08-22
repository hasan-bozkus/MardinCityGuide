using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MardinCityGuide.Mobile.Helpers
{
    public class ResourceKeyToColorConverterHelper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string resourceKey && !string.IsNullOrEmpty(resourceKey))
            {
                if (Application.Current.Resources.TryGetValue(resourceKey, out var resource) && resource is Color color)
                {
                    return color;
                }
            }
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
