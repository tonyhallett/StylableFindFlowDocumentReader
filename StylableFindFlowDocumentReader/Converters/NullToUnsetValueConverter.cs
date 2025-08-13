﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StylableFindFlowDocumentReader
{
    internal class NullToUnsetValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value ?? DependencyProperty.UnsetValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
