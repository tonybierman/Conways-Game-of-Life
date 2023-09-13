using System;
using System.Globalization;
using Bierman.Abm.Model;
using Avalonia.Data.Converters;
using System.Drawing;

namespace Bierman.Abm.Infrastructure;

internal class StateColorConverter : IValueConverter
{
    public static StateColorConverter Instance { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool)
        {
            if ((bool)value) 
            {
                return Brushes.Black;
            }
        }

        return Brushes.Red;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}