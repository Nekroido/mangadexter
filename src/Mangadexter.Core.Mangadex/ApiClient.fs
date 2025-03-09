namespace Mangadexter.Core.Mangadex

module ApiClient =
    open System.Web
    open System
    open FSharp.Control
    open FSharp.Data
    open FsToolkit.ErrorHandling

    open Mangadexter.Core

#if DEBUG
    [<Literal>]
    //let public BaseUrl = "https://api.mangadex.dev/" // <- this one is down atm
    let public BaseUrl = "https://api.mangadex.org/"
#else
    [<Literal>]
    let public BaseUrl = "https://api.mangadex.org/"
#endif

    [<Literal>]
    let MangaListSampleUrl =
        BaseUrl
        + "manga?title=made&includes[]=author&includes[]=artist&includes[]=cover_art&hasAvailableChapters=true&limit=50"

    [<Literal>]
    let ChapterDownloadSampleUrl =
        BaseUrl
        + "at-home/server/4610197f-9185-4838-8f17-406191547806"

    let makeRequestUrl endpoint args : string =
        let query =
            args
            |> Seq.map (fun (key: string, value: string) ->
                let k = HttpUtility.UrlEncode(key)
                let v = HttpUtility.UrlEncode(value)
                $"{k}={v}")
            |> String.concat "&"

        BaseUrl
        |> UriBuilder.fromString
        |> UriBuilder.setPath endpoint
        |> UriBuilder.setQuery query
        |> UriBuilder.toString

    // Manga stuff
    type MangaList = JsonProvider<MangaListSampleUrl>
    type Manga = MangaList.Datum
    type Relationship = MangaList.Relationship

    module Manga =
        type T = Manga

        let getId (manga: T) : Guid = manga.Id

        let getTitle (manga: T) : string =
            manga.Attributes.Title.En
            |> Option.defaultValue "---"

        let getDescription (manga: T) : string option = manga.Attributes.Description.En

        let getCover (manga: T) =
            manga.Relationships
            |> Seq.tryFind (fun r -> r.Type = "cover_art" && r.Attributes.IsSome)

        let getCoverUrl (manga: T) =
             (manga |> getCover)
             |> Option.bind _.Attributes
             |> Option.bind _.FileName
             |> Option.map (sprintf "https://uploads.mangadex.org/covers/%s/%s" ((manga |> getId).ToString()))

        let getYear (manga: T) = manga.Attributes.Year

        let getTags (manga: T) =
            manga.Attributes.Tags
            |> Seq.map (fun tag -> tag.Attributes.Name.En)

        let getCredits (manga: T) : Relationship seq =
            manga.Relationships
            |> Seq.filter (fun r ->
                r.Attributes.IsSome
                && [ "author"; "artist" ] |> Seq.contains r.Type)

        let getFormattedCredits (manga: T) =
            manga
            |> getCredits
            |> Seq.map (fun credit -> credit.Attributes.Value.Name)
            |> Seq.map (Option.defaultValue "")
            |> Seq.filter String.IsNullOrEmpty
            |> String.concat ", "

        let getLastChapterNumber (manga: T) = manga.Attributes.LastChapter

        let getLastVolumeNumber (manga: T) = manga.Attributes.LastVolume

        let getStatus (manga: T) = manga.Attributes.Status

        let toString (manga: T) = manga |> getTitle

        let toDomain (manga: Manga) : Mangadexter.Core.Manga =
            { id = manga |> getId |> Id.fromGuid
              title = manga |> getTitle |> Title
              description =
                manga
                |> getDescription
                |> Option.map Description.create
                |> Option.defaultValue (Description.create "")
              cover = manga |> getCoverUrl |> Option.map Cover.fromString
              status = Ongoing
              year = manga |> getYear |> Option.map uint
              authors = []
              latestChapter =
                manga
                |> getLastChapterNumber
                |> Option.map ChapterNumber
              latestVolume = manga |> getLastVolumeNumber |> Option.map uint
              externalId = manga |> getId |> ExternalId.fromGuid |> Some}

    let public listManga args : Async<Result<MangaList.Root, exn>> =
        asyncResult { return! makeRequestUrl "manga" args |> MangaList.AsyncLoad }

    // Chapter stuff
    type ChapterList = JsonProvider<"chapters-sample.json">
    type Chapter = ChapterList.Datum
    type Translator = ChapterList.Relationship

    module Language =
        let toDomain =
            function
            | "en" -> Language.English
            | "ja" -> Language.Japanese
            | x -> x |> failwithf "Unsupported language %s"
        
        let fromDomain =
            function
            | Language.English -> "en"
            | Language.Japanese -> "ja"

    module Translator =
        type T = Translator

        let getId (t: T) = t.Id

        let getName (t: T) = t.Attributes |> Option.map _.Name

        let toDomain (t: T) : Mangadexter.Core.Publisher =
            { id = t |> getId |> Id.fromGuid; name = t |> getName |> Option.defaultValue "- no name -"; externalId = t |> getId |> ExternalId.Guid |> Some }
    
    module Chapter =
        type T = Chapter
    
        open System.Globalization

        let getId (chapter: T) = chapter.Id
    
        let getChapter (chapter: T) = chapter.Attributes.Chapter
    
        let getFormattedChapterNumber (chapter: T) =
            chapter
            |> getChapter
            |> Option.bind (_.ToString("000.###", CultureInfo.InvariantCulture) >> Some)
            |> Option.defaultValue "-"
    
        let getFormattedChapter (chapter: T) =
            $"Chapter {chapter |> getFormattedChapterNumber}"
    
        let getTitle (chapter: T) =
            chapter.Attributes.Title |> Option.defaultValue ""
    
        let getVolume (chapter: T) = chapter.Attributes.Volume
    
        let getFormattedVolumeNumber (chapter: T) =
            chapter
            |> getVolume
            |> Option.bind (_.ToString("00") >> Some)
    
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
            |> Seq.map _.Attributes.Value.Name
            |> String.concat ", "
    
        let getPublishDate (chapter: T) = chapter.Attributes.PublishAt

        let getTotalPages (chapter: T) = chapter.Attributes.Pages
    
        //let getHash (chapter: T) = chapter.Attributes.Hash.ToString("N")
    
        let getFormattedTitle (chapter: T) =
            [| chapter |> getFormattedVolume
               chapter |> getFormattedChapter
               chapter |> getTitle |]
            |> String.concat " - "
    
        let toString (chapter: T) =
            $"%s{chapter |> getFormattedTitle}[%s{chapter |> getTranslatedLanguage}]"

        let toDomain (chapter: T) : Mangadexter.Core.Chapter =
            { id = chapter |> getId |> Id.fromGuid
              number = chapter.Attributes.Chapter |> Option.map ChapterNumber
              volume = chapter.Attributes.Volume |> Option.map uint
              title = chapter.Attributes.Title |> Option.map Title
              description = None
              publishedAt = chapter |> getPublishDate
              totalPages = chapter |> getTotalPages |> uint
              externalId = None }
    
    let public listChapters args : Async<Result<ChapterList.Root, exn>> =
        asyncResult { return! makeRequestUrl "chapter" args |> ChapterList.AsyncLoad }

    // Chapter download stuff
    type ChapterDownload = JsonProvider<ChapterDownloadSampleUrl>
    type DownloadInfo = ChapterDownload.Root

    module ChapterDownloadInfo =
        type T = DownloadInfo

        let getBaseUrl (info: T) = info.BaseUrl

        let getChapterHash (info: T) = info.Chapter.Hash |> stringf "n"

        let getPages quality (info: T) =
            match quality with
            | Quality.High -> info.Chapter.Data
            | Quality.Low -> info.Chapter.DataSaver
            
    let public getChapterDownloadInfo (chapterId: Guid) : Async<Result<DownloadInfo, exn>> =
        asyncResult { return! makeRequestUrl $"at-home/server/%A{chapterId}" [] |> ChapterDownload.AsyncLoad }

    let getPageDownloadUrl downloadInfo quality page =
        let quality =
            match quality with
            | Quality.High -> "data"
            | Quality.Low -> "data-saver"
    
        $"{downloadInfo |> ChapterDownloadInfo.getBaseUrl}/{quality}/{downloadInfo |> ChapterDownloadInfo.getChapterHash}/{page}"
    