using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using Mangadexter.Gui.Models;
using System.Linq;

namespace Mangadexter.Gui.DataValueConverters;

public class JoinAuthors : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IEnumerable<Author> authors || !targetType.IsAssignableTo(typeof(string)))
        {
            // converter used for the wrong type
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        return string.Join(", ", authors.Select(x => x.Name));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
