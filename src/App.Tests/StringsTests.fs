module App.Tests.StringsTests

open Xunit
open Strings

[<Fact>]
let ``Strings.GetString returns value for valid key`` () =
    let result = Strings.GetString "Pages.Root.SelectAction"
    Assert.Equal("Select action", result)

[<Fact>]
let ``Strings.GetString handles Pages.Root.Actions keys`` () =
    let searchManga = Strings.GetString "Pages.Root.Actions.SearchManga"
    let preferences = Strings.GetString "Pages.Root.Actions.Preferences"
    let exit = Strings.GetString "Pages.Root.Actions.Exit"
    
    Assert.NotNull(searchManga)
    Assert.NotNull(preferences)
    Assert.NotNull(exit)

[<Fact>]
let ``Strings.GetString returns values for all Manga page keys`` () =
    let selectChapters = Strings.GetString "Pages.Manga.Actions.SelectChapters"
    let downloadAll = Strings.GetString "Pages.Manga.Actions.DownloadAllChapters"
    
    Assert.NotNull(selectChapters)
    Assert.NotNull(downloadAll)

[<Fact>]
let ``Strings.GetString returns values for all Preferences keys`` () =
    let savePath = Strings.GetString "Pages.Preferences.Actions.SavePath"
    let language = Strings.GetString "Pages.Preferences.Actions.Language"
    let quality = Strings.GetString "Pages.Preferences.Actions.Quality"
    
    Assert.NotNull(savePath)
    Assert.NotNull(language)
    Assert.NotNull(quality)
