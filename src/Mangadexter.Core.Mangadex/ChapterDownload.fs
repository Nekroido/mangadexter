namespace Mangadexter.Core.Mangadex

[<RequireQualifiedAccess>]
module ChapterDownload =
    open FsToolkit.ErrorHandling

    open Mangadexter.Core
    open Mangadexter.Core.ChapterDownloadRequests

//let public getMangaChapters: GetPagesForChapter =
//    fun args ->
//        asyncResult {
//            return!
//                args.chapterId
//                |> Id.value
//                |> ApiClient.getChapterDownloadInfo
//                |> AsyncResult.map (fun r ->
//                    { items = r.Data |> Seq.map ApiClient.Chapter.toDomain }: GetPagesForChapterResult)
//        }
