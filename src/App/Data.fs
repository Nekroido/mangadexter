module Data

open System.Web

open Utils

[<Literal>]
let BaseUrl = "https://api.mangadex.org/"

[<Literal>]
let ChapterServerSampleUrl = "chapter-server-sample.json"

[<Literal>]
let MangaListSampleUrl =
    BaseUrl
    + "manga?title=made&includes[]=author&includes[]=artist&hasAvailableChapters=true"

[<Literal>]
let ChapterListSample = "chapters-sample.json"

let makeRequestUrl endpoint args : string =
    let query =
        args
        |> Seq.map
            (fun (key: string, value: string) ->
                let k = HttpUtility.UrlEncode(key)
                let v = HttpUtility.UrlEncode(value)
                $"{k}={v}")
        |> String.concat "&"

    BaseUrl
    |> UriBuilder.fromString
    |> UriBuilder.setPath endpoint
    |> UriBuilder.setQuery query
    |> UriBuilder.toString

open FSharp.Data

type ChapterServer = JsonProvider<ChapterServerSampleUrl>

type ChapterList = JsonProvider<"chapters-sample.json">

type MangaList = JsonProvider<MangaListSampleUrl>

type Server = ChapterServer.Root
type Chapter = ChapterList.Datum
type Manga = MangaList.Datum
type Relationship = MangaList.Relationship

module Manga =
    type T = Manga

    /// <summary>Gets the localized title of a manga.</summary>
    /// <param name="manga">Manga object</param>
    /// <returns>Manga title in English, or "---" if not available</returns>
    let getTitle (manga: T) : string =
        manga.Attributes.Title.En
        |> Option.defaultValue "---"

    /// <summary>Gets the publication year of a manga.</summary>
    /// <param name="manga">Manga object</param>
    /// <returns>Publication year as uint option</returns>
    let getYear (manga: T) = manga.Attributes.Year

    /// <summary>Gets all tags associated with a manga.</summary>
    /// <param name="manga">Manga object</param>
    /// <returns>Sequence of localized tag names</returns>
    let getTags (manga: T) =
        manga.Attributes.Tags
        |> Seq.map (fun tag -> tag.Attributes.Name.En)

    /// <summary>Gets author and artist relationships for a manga.</summary>
    /// <param name="manga">Manga object</param>
    /// <returns>Sequence of author/artist relationship objects</returns>
    let getCredits (manga: T) : Relationship seq =
        manga.Relationships
        |> Seq.filter
            (fun r ->
                r.Attributes.IsSome
                && [ "author"; "artist" ] |> Seq.contains r.Type)

    /// <summary>Gets formatted comma-separated list of credits.</summary>
    /// <param name="manga">Manga object</param>
    /// <returns>Formatted string of author and artist names</returns>
    let getFormattedCredits (manga: T) =
        manga
        |> getCredits
        |> Seq.map (fun credit -> credit.Attributes.Value.Name)
        |> String.join ", "

    /// <summary>Gets the last chapter number available for a manga.</summary>
    /// <param name="manga">Manga object</param>
    /// <returns>Last chapter number as string option</returns>
    let getLastChapterNumber (manga: T) = manga.Attributes.LastChapter

    /// <summary>Gets the publication status of a manga.</summary>
    /// <param name="manga">Manga object</param>
    /// <returns>Status string (e.g., "ongoing", "completed")</returns>
    let getStatus (manga: T) = manga.Attributes.Status

    /// <summary>Converts manga object to its string representation.</summary>
    /// <param name="manga">Manga object</param>
    /// <returns>Manga title</returns>
    let toString (manga: T) = manga |> getTitle

module Chapter =
    type T = Chapter

    open Preferences

    /// <summary>Gets page image URLs for a chapter at specified quality.</summary>
    /// <param name="quality">Quality level (High or Low resolution)</param>
    /// <param name="chapter">Chapter object</param>
    /// <returns>Sequence of page filenames</returns>
    let getPages quality (chapter: T) =
        match quality with
        | Quality.High -> chapter.Attributes.Data
        | Quality.Low -> chapter.Attributes.DataSaver

    /// <summary>Gets the chapter number.</summary>
    /// <param name="chapter">Chapter object</param>
    /// <returns>Chapter number as decimal option</returns>
    let getChapter (chapter: T) = chapter.Attributes.Chapter

    /// <summary>Gets formatted chapter number with padding.</summary>
    /// <param name="chapter">Chapter object</param>
    /// <returns>Formatted chapter number string (e.g., "001.000")</returns>
    let getFormattedChapterNumber (chapter: T) =
        (chapter |> getChapter).ToString("000.###")

    /// <summary>Gets formatted chapter display string.</summary>
    /// <param name="chapter">Chapter object</param>
    /// <returns>Display string like "Chapter 001.000"</returns>
    let getFormattedChapter (chapter: T) =
        $"Chapter {chapter |> getFormattedChapterNumber}"

    /// <summary>Gets the chapter title.</summary>
    /// <param name="chapter">Chapter object</param>
    /// <returns>Chapter title or empty string if not available</returns>
    let getTitle (chapter: T) =
        chapter.Attributes.Title |> Option.defaultValue ""

    let getVolume (chapter: T) = chapter.Attributes.Volume

    let getFormattedVolumeNumber (chapter: T) =
        chapter
        |> getVolume
        |> Option.bind (fun volume -> volume.ToString("00") |> Some)

    let getFormattedVolume (chapter: T) =
        chapter
        |> getVolume
        |> Option.bind (fun volume -> $"Volume {volume}" |> Some)
        |> Option.defaultValue ""

    let getTranslatedLanguage (chapter: T) = chapter.Attributes.TranslatedLanguage

    let getTranslatorGroups (chapter: T) =
        chapter.Relationships
        |> Seq.filter (fun r -> r.Attributes.IsSome && r.Type = "scanlation_group")

    let getFormattedTranslatorGroup (chapter: T) =
        chapter
        |> getTranslatorGroups
        |> Seq.map (fun r -> r.Attributes.Value.Name)
        |> String.join ", "

    let getPublishDate (chapter: T) = chapter.Attributes.PublishAt

    let getHash (chapter: T) = chapter.Attributes.Hash.ToString("N")

    let getFormattedTitle (chapter: T) =
        [| chapter |> getFormattedVolume
           chapter |> getFormattedChapter
           chapter |> getTitle |]
        |> String.join " - "

    let toString (chapter: T) =
        $"%s{chapter |> getFormattedTitle}[%s{chapter |> getTranslatedLanguage}]"
