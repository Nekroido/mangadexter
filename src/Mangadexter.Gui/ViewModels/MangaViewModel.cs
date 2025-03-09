using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.Input;
using Mangadexter.Gui.Models;
using Mangadexter.Gui.Repositories;

namespace Mangadexter.Gui.ViewModels;

public class MangaViewModel : ViewModelBase
{
    private readonly IMangaRepository _mangaRepository;

    private Manga? _manga = null;
    private ObservableCollection<Chapter> _chapters = [];
    private bool _isLoading = false;
    private ObservableGroupedCollection<string, ObservableGroupedCollection<string, Chapter>> _groupedChapters = [];

    public MangaViewModel(IMangaRepository mangaRepository)
    {
        this._mangaRepository = mangaRepository;

        LoadChaptersCommand = new AsyncRelayCommand(LoadChapters);
    }

    public async Task LoadChapters()
    {
        if (_manga == null)
            return;

        IsLoading = true;

        var chapters = await _mangaRepository.GetChaptersForMangaId(_manga.Id, SupportedLanguage.English);

        var groupedByChapter = chapters.GroupBy(static c => c.GetChapterLabel()).OrderBy(static g => g.Key);
        var groupedByVolume = groupedByChapter.Select(x => x.GroupBy(static c => c.GetVolumeLabel()).OrderBy(static g => g.Key));

        //var groupedChapters = new ObservableGroupedCollection<string, ObservableGroupedCollection<string, Chapter>>(groupedByVolume);

        Chapters.Clear();
        chapters.ForEach(Chapters.Add);

        IsLoading = false;
    }

    public IAsyncRelayCommand LoadChaptersCommand { get; }

    public Manga? Manga
    {
        get => _manga;
        set => SetProperty(ref _manga, value);
    }

    public ObservableCollection<Chapter> Chapters
    {
        get => _chapters;
        set => SetProperty(ref _chapters, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }
}
