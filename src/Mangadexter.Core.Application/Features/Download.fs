module Download

open Mangadexter.Core

type DownloadState =
    { currentDownloads: Result<Download, DownloadError> list }

module PublicationPickers =
    let latestUpdate (publications: Publication list) = publications |> List.maxBy _.publishedAt
