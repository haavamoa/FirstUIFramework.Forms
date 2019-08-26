﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheFramework.Resources.Converters
{
    public class ValuesAdditionConverter : IValueConverter, IMarkupExtension
    {
        public int ToAdd { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out var intValue))
            {
                return intValue + ToAdd;
            }

            throw new ArgumentException("Value should be of type int");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}