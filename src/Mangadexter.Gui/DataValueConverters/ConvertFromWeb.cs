using Avalonia.Data.Converters;
using Mangadexter.Gui.Helpers;
using System;
using System.Globalization;

namespace Mangadexter.Gui.DataValueConverters;

public class ConvertFromWeb : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // return "avares://Mangadexter.Gui/Assets/cover.jpg";

        if (value is Uri uri)
        {
            return ImageHelper.LoadFromWeb(uri);
        }

        return ImageHelper.LoadFromResource(new("avares://Mangadexter.Gui/Assets/cover.jpg"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
