namespace Mangadexter.Core

open System

type PublicationId = Id
type PublisherId = Id

type Publication =
    { id: PublicationId
      cover: Cover option
      publishedBy: Publisher
      originalLanguage: Language
      translatedLanguage: Language option
      publishedAt: DateTimeOffset
      externalId: ExternalId option }

and Publisher =
    { id: PublisherId
      name: string
      externalId: ExternalId option }

module PublicationRequests =
    type PublicationRequestError =
        | PublicationNotFound
        | ChapterNotFound

    type GetPublicationById = GetPublicationByIdArgs -> Async<Result<GetPublicationByIdResult, PublicationRequestError>>

    and GetPublicationByIdArgs =
        | Id of Id
        | ExternalId of ExternalId

    and GetPublicationByIdResult = { publication: Publication }

    type GetAllPublicationsForChapter =
        GetAllPublicationsForChapterArgs -> Async<Result<GetAllPublicationsForChapterResult, PublicationRequestError>>

    and GetAllPublicationsForChapterArgs =
        | ChapterId of Id
        | ExternalChapterId of ExternalId

    and GetAllPublicationsForChapterResult = { publications: Publication [] }
