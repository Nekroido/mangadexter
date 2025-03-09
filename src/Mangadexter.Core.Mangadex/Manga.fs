namespace Mangadexter.Core.Mangadex

[<RequireQualifiedAccess>]
module Manga =
    open FsToolkit.ErrorHandling

    open Mangadexter.Core.MangaRequests

    let public findManga: FindManga =
        fun args ->
            asyncResult {
                return!
                    [ ("title", args.title)
                      ("order[relevance]", "desc")
                      ("includes[]", "cover_art")
                      ("includes[]", "artist")
                      ("includes[]", "artist")
                      ("availableTranslatedLanguage[]",
                       args.preferredLanguage
                       |> Option.map ApiClient.Language.fromDomain
                       |> Option.defaultValue "[]")
                      ("limit", $"%d{args.take}")
                      ("offset", $"%d{args.skip}") ]
                    |> ApiClient.listManga
                    |> AsyncResult.mapError (fun _ -> MangaRequestError.MangaNotFound)
                    |> AsyncResult.map (fun r -> { items = r.Data |> Seq.map ApiClient.Manga.toDomain })
            }

    let public getMangaById: GetMangaById = fun args -> failwith "todo"
