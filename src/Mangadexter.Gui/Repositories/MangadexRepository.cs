using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;

using Mangadexter.Gui.Models;
using System.Threading;
using static Mangadexter.Core.MangaRequests;
using static Mangadexter.Core.ChapterRequests;

namespace Mangadexter.Gui.Repositories;

public class MangadexRepository : IMangaRepository
{
    private readonly Func<FindMangaArgs, FSharpAsync<FSharpResult<FindMangaResult, MangaRequestError>>> _findManga;
    private readonly Func<GetChaptersForMangaArgs, FSharpAsync<FSharpResult<GetChaptersForMangaResult, ChapterRequestError>>> _getMangaChapters;

    public MangadexRepository()
    {
        _findManga = Mangadexter.Core.Mangadex.Manga.findManga;
        _getMangaChapters = Mangadexter.Core.Mangadex.Chapter.getMangaChapters;
    }

    public async Task<List<Manga>> FindMangaByTitle(string title, SupportedLanguage supportedLanguage, uint? take, uint? skip)
    {
        var cancellationToken = new CancellationToken();

        var result = await FSharpAsync.StartAsTask(
            _findManga.Invoke(new(title, Core.Language.English, null, take ?? 50, skip ?? 0)),
            FSharpOption<TaskCreationOptions>.None,
            FSharpOption<CancellationToken>.Some(cancellationToken));

        if (result.IsError)
        {
            throw new Exception(result.ErrorValue.ToString());
        }

        return result.ResultValue.items.Select(Manga.FromDomain).ToList();
    }

    public Task<Manga> GetMangaById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Chapter>> GetChaptersForMangaId(string mangaId, SupportedLanguage preferredLanguage, uint? take, uint? skip)
    {
        var cancellationToken = new CancellationToken();

        var result = await FSharpAsync.StartAsTask(
            _getMangaChapters.Invoke(new(Core.Id.NewId(mangaId), Core.Language.English, take ?? 50, skip ?? 0)),
            FSharpOption<TaskCreationOptions>.None,
            FSharpOption<CancellationToken>.Some(cancellationToken));

        if (result.IsError)
        {
            throw new Exception(result.ErrorValue.ToString());
        }

        return result.ResultValue.items.Select(Chapter.FromDomain).ToList();
    }
}
