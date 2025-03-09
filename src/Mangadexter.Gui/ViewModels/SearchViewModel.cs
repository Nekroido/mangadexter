using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Mangadexter.Gui.Models;
using Mangadexter.Gui.Repositories;

namespace Mangadexter.Gui.ViewModels;

public partial class SearchViewModel : ViewModelBase
{
    private readonly IMangaRepository _mangaRepository;

    public SearchViewModel(IMangaRepository mangaRepository)
    {
        this._mangaRepository = mangaRepository;

        SearchCommand = new AsyncRelayCommand(Search);
    }

    private string _searchTerm = "";
    private ObservableCollection<Manga> _items = [];
    private bool _isLoading = false;

    public string SearchTerm
    {
        get => _searchTerm;
        set => SetProperty(ref _searchTerm, value);
    }

    public ObservableCollection<Manga> Items
    {
        get { return _items; }
        set { SetProperty(ref _items, value); }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public IAsyncRelayCommand SearchCommand { get; }

    public async Task Search()
    {
        if (string.IsNullOrWhiteSpace(_searchTerm))
            return;

        IsLoading = true;

        var items = await _mangaRepository.FindMangaByTitle(SearchTerm, SupportedLanguage.English);

        Items.Clear();
        items.ForEach(Items.Add);

        IsLoading = false;
    }
}
