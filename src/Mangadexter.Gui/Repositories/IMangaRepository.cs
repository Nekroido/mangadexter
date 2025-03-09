using Mangadexter.Gui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangadexter.Gui.Repositories;

public interface IMangaRepository
{
    Task<List<Manga>> FindMangaByTitle(string title, SupportedLanguage supportedLanguage, uint? take = null, uint? skip = null);

    Task<Manga> GetMangaById(string id);

    Task<List<Chapter>> GetChaptersForMangaId(string mangaId, SupportedLanguage supportedLanguage, uint? take = null, uint? skip = null);
}
