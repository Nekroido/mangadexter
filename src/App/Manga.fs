module Manga

open Data
open Preferences

/// <summary>Fetches paginated list of manga from Mangadex API.</summary>
/// <param name="args">Query arguments (title, language, includes, etc.)</param>
/// <returns>Async result containing manga list or error</returns>
let listManga args =
    async { return! makeRequestUrl "manga" args |> MangaList.AsyncLoad }

/// <summary>Searches for manga by title with language filter.</summary>
/// <param name="limit">Maximum results to return</param>
/// <param name="offset">Pagination offset</param>
/// <param name="language">Preferred language (English or Japanese)</param>
/// <param name="query">Search query string</param>
/// <returns>Async result containing matching manga or error</returns>
let searchManga limit offset language (query: string) =
    let preferredLanguage =
        language
        |> function
            | Language.English -> "en"
            | Language.Japanese -> "ja"

    [ ("title", query)
      ("order[relevance]", "desc")
      ("includes[]", "author")
      ("includes[]", "artist")
      ("availableTranslatedLanguage[]", preferredLanguage)
      ("limit", $"%d{limit}")
      ("offset", $"%d{offset}") ]
    |> listManga
