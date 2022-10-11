﻿using System.Globalization;

namespace PSI_MobileApp;

public class Converter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum)
        {
            return value.ToString();
        }
        else return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}