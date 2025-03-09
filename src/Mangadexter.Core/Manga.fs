namespace Mangadexter.Core

open System

type MangaId = Id

type Manga =
    { id: MangaId
      title: Title
      description: Description
      cover: Cover option
      status: Status
      year: uint option
      authors: Author list
      latestChapter: ChapterNumber option
      latestVolume: uint option
      externalId: ExternalId option }

[<RequireQualifiedAccess>]
module Manga =
    type Command =
        | Add of manga: Manga
        | UpdateCover of id: MangaId * coverPath: Uri
        | Delete of id: MangaId

    type Event =
        | MangaAdded of manga: Manga
        | CoverUpdated of manga: Manga
        | MangaDeleted of id: MangaId

    type Error =
        | MangaNotFound of id: MangaId
        | InvalidCover of coverPath: string option

module MangaRequests =
    type MangaRequestError = MangaNotFound

    type FindManga = FindMangaArgs -> Async<Result<FindMangaResult, MangaRequestError>>

    and FindMangaArgs =
        { title: string
          preferredLanguage: Language option
          status: Status option
          take: uint
          skip: uint }

    and FindMangaResult = { items: Manga seq }

    type GetMangaById = GetMangaByIdArgs -> Async<Result<GetMangaByIdResult, MangaRequestError>>
    and GetMangaByIdArgs = { mangaId: MangaId }
    and GetMangaByIdResult = { manga: Manga }
