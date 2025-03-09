using Microsoft.FSharp.Core;

namespace Mangadexter.Gui.Helpers;

public static class OptionModule
{
    public static T? ToNullable<T>(this FSharpOption<T> option)
    {
        try
        {
            return option.Value;
        }
        catch
        {
            return default;
        }
    }
}
