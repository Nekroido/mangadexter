module App.Tests.UtilsTests

open Xunit
open Utils

[<Fact>]
let ``String.join concatenates strings with separator`` () =
    let words = ["hello"; "world"; "test"]
    let result = String.join " " words
    Assert.Equal("hello world test", result)

[<Fact>]
let ``String.join handles empty list`` () =
    let result = String.join "," []
    Assert.Equal("", result)

[<Fact>]
let ``String.join handles single element`` () =
    let result = String.join "-" ["single"]
    Assert.Equal("single", result)

[<Fact>]
let ``UriBuilder creates valid URI`` () =
    let builder = System.UriBuilder("https", "api.example.com", 443, "/endpoint")
    let uri = builder.ToString()
    Assert.Contains("api.example.com", uri)
    Assert.Contains("endpoint", uri)

[<Fact>]
let ``UriBuilder setPath works correctly`` () =
    let builder = System.UriBuilder("https", "api.example.com")
    builder.Path <- "/manga"
    let result = builder.ToString()
    Assert.Contains("/manga", result)
