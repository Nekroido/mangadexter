module Pages.Search

open System
open Console
open Data
open Utils

let askForMangaTitle () =
    Console.clear ()
    Strings.Strings.GetString "Pages.Search.AskMangaTitle" |> Console.ask

let searchMangaByTitle title =
    let preferredLanguage =
        Preferences.getLanguage
        <| Preferences.loadPreferences ()

    title
    |> Manga.searchManga 30 0 preferredLanguage
    |> Console.status (Strings.Strings.GetString "Pages.Search.SearchStatus" |> sprintf "%s" |> fun fmt -> fmt.Replace("{0}", title))

let selectManga (listResult: MangaList.Root) =
    SelectionPrompt.create<Manga> (Strings.Strings.GetString "Pages.Search.FoundWorks")
    |> SelectionPrompt.addChoices listResult.Data
    |> SelectionPrompt.withConverter (Func<Manga, string>(Manga.getTitle))
    |> Console.prompt

let initialize returnAction =
    askForMangaTitle ()
    |> searchMangaByTitle
    |> Result.proceedIfOk
    |> selectManga
    |> Manga.initialize returnAction
