namespace Mangadexter.Core

module ChapterDownloadRequests =
    type GetPagesForChapter = GetPagesForChapterArgs -> Async<Result<GetPagesForChapterResult, exn>>

    and GetPagesForChapterArgs =
        { mangaId: Id
          chapterId: Id
          quality: Quality }

    and GetPagesForChapterResult =
        { baseUrl: string
          pages: (uint * string) seq }
