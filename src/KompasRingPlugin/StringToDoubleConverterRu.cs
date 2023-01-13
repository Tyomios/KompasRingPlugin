using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace KompasRingPlugin;

public class StringToDoubleConverterRu : IValueConverter
{
    public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        return $"{parameter ??= "Значение"} - {value}";
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        var prepareValue = value as string;
        if (double.TryParse(prepareValue, out double num))
        {
            return num;
        }
        return DependencyProperty.UnsetValue;

    }
}