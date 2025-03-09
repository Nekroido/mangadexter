using Avalonia.Controls;
using Mangadexter.Gui.ViewModels;

namespace Mangadexter.Gui.Helpers;

internal class NavigationHelper
{
    private readonly Control _control;

    public NavigationHelper(Control control)
    {
        _control = control;
    }

    public MainViewModel? GetMainViewModel()
    {
        if (TopLevel.GetTopLevel(_control)?.DataContext is MainViewModel mainViewModel)
        {
            return mainViewModel;
        }

        return null;
    }
}
