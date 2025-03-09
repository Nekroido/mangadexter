using Avalonia.Controls;
using Avalonia.Interactivity;

using Mangadexter.Gui.Helpers;
using Mangadexter.Gui.Models;

namespace Mangadexter.Gui.Views
{
    public partial class SearchView : UserControl
    {
        private NavigationHelper _navigationHelper;

        public SearchView()
        {
            InitializeComponent();

            _navigationHelper = new NavigationHelper(this);
        }

        private void ListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Manga manga && manga != null)
            {
                _navigationHelper.GetMainViewModel()?.NavigateToManga(manga);
            }
        }
    }
}
