using System;

using Mangadexter.Gui.ViewModels;

namespace Mangadexter.Gui.Helpers;

public class ViewModelLocator(IServiceProvider serviceProvider)
{
    public ViewModelBase GetForView(object view)
    {
        var name = view.GetType().FullName!.Replace("View", "ViewModel");
        var type = Type.GetType(name) ?? throw new Exception($"No ViewModel {name} found for view");

        return serviceProvider.GetService(type) as ViewModelBase ?? (ViewModelBase)Activator.CreateInstance(type)!;
    }
}
