module App.Tests.PreferencesTests

open Xunit
open Preferences

[<Fact>]
let ``Language.English has string representation`` () =
    let result = Language.toString Language.English
    Assert.NotNull(result)

[<Fact>]
let ``Language.Japanese has string representation`` () =
    let result = Language.toString Language.Japanese
    Assert.NotNull(result)

[<Fact>]
let ``Quality.High has string representation`` () =
    let result = Quality.toString Quality.High
    Assert.NotNull(result)

[<Fact>]
let ``Quality.Low has string representation`` () =
    let result = Quality.toString Quality.Low
    Assert.NotNull(result)

[<Fact>]
let ``Default preferences have valid values`` () =
    let prefs = loadPreferences ()
    Assert.NotNull(prefs.SavePath)
    Assert.NotNull(prefs.Quality)
    Assert.NotNull(prefs.Language)
