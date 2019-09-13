using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheFramework.Resources.Converters
{
    public class ValuesSubtractionConverter : IMarkupExtension, IValueConverter
    {
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out var intValue) && int.TryParse(parameter.ToString(), out var toSubtract))
            {
                return intValue - toSubtract;
            }

            throw new ArgumentException("Value / Parameter should be of type int");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
