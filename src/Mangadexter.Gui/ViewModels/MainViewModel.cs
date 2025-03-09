using Mangadexter.Gui.Models;
using Mangadexter.Gui.Repositories;

namespace Mangadexter.Gui.ViewModels;

public partial class MainViewModel(IMangaRepository mangaRepository) : ViewModelBase
{
    private ViewModelBase _currentContentViewModel = new SearchViewModel(mangaRepository);

    private SearchViewModel _searchViewModel = new SearchViewModel(mangaRepository);
    private MangaViewModel? _mangaViewModel;

    public bool IsLoading { get; set; }

    public ViewModelBase CurrentContentViewModel
    {
        get => _currentContentViewModel;
        set => SetProperty(ref _currentContentViewModel, value);
    }

    public void NavigateToSearch()
    {
        CurrentContentViewModel = _searchViewModel;
    }

    public void NavigateToManga(Manga manga)
    {
        _mangaViewModel = new MangaViewModel(mangaRepository) { Manga = manga };
        CurrentContentViewModel = _mangaViewModel;
    }
}
