module File

open System.IO
open System.Text.Json

open System.Text.Json.Serialization
open Data
open Utils

type Credits =
    { person: string
      role: string
      primary: bool }

type Metadata =
    { title: string
      series: string
      credits: Credits seq
      publicationYear: int
      tags: string seq
      volume: int option
      issue: decimal }

let private mapCredits (entity: Relationship) =
    let typeToRole t =
        match t with
        | "author" -> "Writer"
        | "artist" -> "Artist"
        | _ -> t

    { person = entity.Attributes.Value.Name
      role = entity.Type |> typeToRole
      primary = true }

let private mapChapterMetadata (chapter: Chapter) (manga: Manga) =
    { title = $"{manga |> Manga.getTitle} - {chapter |> Chapter.getFormattedTitle}"
      series = manga |> Manga.getTitle
      credits = manga |> Manga.getCredits |> Seq.map mapCredits
      publicationYear = manga |> Manga.getYear
      tags = manga |> Manga.getTags
      volume = chapter |> Chapter.getVolume
      issue = chapter |> Chapter.getChapter }

let serialize metadata =
    let options = JsonSerializerOptions()
    options.Converters.Add(JsonFSharpConverter())

    JsonSerializer.Serialize({| ``ComicBookInfo/1.0`` = metadata |}, options)

let createStream () = new MemoryStream()

type CBZSettings =
    { SavePath: string
      Manga: Manga
      Chapter: Chapter
      Pages: seq<string * Stream> }

type CBZBuilder() =
    member _.Yield _ =
        { SavePath = Unchecked.defaultof<string>
          Manga = Unchecked.defaultof<Manga>
          Chapter = Unchecked.defaultof<Chapter>
          Pages = [] }

    [<CustomOperation("save_path")>]
    member _.SetSavePath(settings, path) = { settings with SavePath = path }

    [<CustomOperation("with_manga")>]
    member _.SetManga(settings, manga) = { settings with Manga = manga }

    [<CustomOperation("with_chapter")>]
    member _.SetChapter(settings, chapter) = { settings with Chapter = chapter }

    [<CustomOperation("with_pages")>]
    member _.SetPages(settings, pages) = { settings with Pages = pages }

    member _.Run settings =
        let metadata =
            mapChapterMetadata settings.Chapter settings.Manga
            |> serialize

        settings.SavePath |> Directory.createForPath

        if File.Exists settings.SavePath then
            File.Delete settings.SavePath

        // Create temporary directory for CBZ assembly
        let tempDir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.Guid.NewGuid().ToString())
        System.IO.Directory.CreateDirectory(tempDir) |> ignore

        try
            // Write metadata file
            let metadataPath = System.IO.Path.Combine(tempDir, "metadata.xml")
            System.IO.File.WriteAllText(metadataPath, metadata)

            // Write page files to temp directory
            settings.Pages
            |> Seq.iteri
                (fun index (filename, page) ->
                    page.Seek(0, SeekOrigin.Begin) |> ignore
                    let entryName = $"%03d{index}{filename |> Path.getFileExtension}"
                    let tempPath = System.IO.Path.Combine(tempDir, entryName)
                    use fileStream = System.IO.File.Create(tempPath)
                    page.CopyTo(fileStream))

            // Create CBZ (zip) file from temp directory
            use zip = new ICSharpCode.SharpZipLib.Zip.ZipFile(settings.SavePath)
            
            let files = System.IO.Directory.GetFiles(tempDir)
            for file in files do
                zip.Add(file, System.IO.Path.GetFileName(file)) |> ignore

            zip.CommitUpdate()
            zip.Close()
        finally
            // Clean up temporary directory
            try
                System.IO.Directory.Delete(tempDir, true)
            with _ -> ()

let cbzBuilder = CBZBuilder()
