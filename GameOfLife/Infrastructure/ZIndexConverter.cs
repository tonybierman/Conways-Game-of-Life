using System;
using System.Globalization;
using Bierman.Abm.Model;
using Avalonia.Data.Converters;

namespace Bierman.Abm.Infrastructure;

internal class ZIndexConverter : IValueConverter
{
    public static ZIndexConverter Instance { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Agent)
            return 1;

        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}