namespace Mangadexter.Core.Mangadex

[<RequireQualifiedAccess>]
module Chapter =
    open FsToolkit.ErrorHandling

    open Mangadexter.Core
    open Mangadexter.Core.ChapterRequests

    let public getMangaChapters: GetChaptersForManga =
        fun args ->
            asyncResult {
                return!
                    [ ("manga", $"%s{args.mangaId |> Id.value}")
                      ("translatedLanguage[]",
                       args.preferredLanguage
                       |> Option.map ApiClient.Language.fromDomain
                       |> Option.defaultValue "[]")
                      ("order[chapter]", "asc")
                      ("includes[]", "scanlation_group")
                      ("limit", $"%d{args.take}")
                      ("offset", $"%d{args.skip}") ]
                    |> ApiClient.listChapters
                    |> AsyncResult.map (fun r ->
                        { items = r.Data |> Seq.map ApiClient.Chapter.toDomain }: GetChaptersForMangaResult)
                    |> AsyncResult.mapError (fun _ -> ChapterRequestError.ChapterNotFound)
            }
