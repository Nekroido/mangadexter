using Avalonia.Controls.Templates;
using Avalonia.Controls;
using System;

using Mangadexter.Gui.ViewModels;

namespace Mangadexter.Gui.Helpers;

public class ViewLocator(IServiceProvider serviceProvider) : IDataTemplate
{
    public Control Build(object? data)
    {
        var name = data!.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            var control = (Control?)serviceProvider.GetService(type);

            return control ?? (Control)Activator.CreateInstance(type)!;
        }
        else
        {
            return new TextBlock { Text = "Not Found: " + name };
        }
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
