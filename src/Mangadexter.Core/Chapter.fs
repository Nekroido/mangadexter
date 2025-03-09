namespace Mangadexter.Core

open System

type ChapterId = Id

type Chapter =
    { id: ChapterId
      number: ChapterNumber option
      volume: uint option
      title: Title option
      description: Description option
      publishedAt: DateTimeOffset
      totalPages: uint
      externalId: ExternalId option }

module ChapterRequests =
    type ChapterRequestError =
        | ChapterNotFound
        | MangaNotFound
        | Exception of exn

    type GetChaptersForManga = GetChaptersForMangaArgs -> Async<Result<GetChaptersForMangaResult, ChapterRequestError>>

    and GetChaptersForMangaArgs =
        { mangaId: Id
          preferredLanguage: Language option
          take: uint
          skip: uint }

    and GetChaptersForMangaResult = { items: Chapter seq }
