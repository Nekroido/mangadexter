namespace Mangadexter.Core

open MangaRequests
open ChapterRequests
open PublicationRequests

type Quality =
    | Low
    | High

type PublicationPicker = Publication [] -> Publication

type Download = { publication: Publication; quality: Quality }

type DownloadError =
    | MangaError of MangaRequestError
    | ChapterError of ChapterRequestError
    | PublicationError of PublicationRequestError

type DownloadCommand =
    | Manga of mangaId: Id * Quality
    | Chapters of chapterIds: Id array * PublicationPicker * Quality
    | Publication of publicationId: Id * Quality

module PublicationPicker =
    let latestPublication: PublicationPicker =
        fun (translations: Publication []) ->
            translations
            |> List.ofArray
            |> List.maxBy _.publishedAt
