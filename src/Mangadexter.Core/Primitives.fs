namespace Mangadexter.Core

open System

type Status =
    | Ongoing
    | Finished
    | Canceled
    | Hiatus

type Id = Id of string

type ExternalId =
    | Guid of Guid
    | String of string

type Title = Title of string
type Description = Description of string
type Cover = Cover of Uri
type ChapterNumber = ChapterNumber of decimal

type AuthorName = AuthorName of string

type Author =
    | Artist of AuthorName
    | Writer of AuthorName
    member x.TryGetArtist() =
        match x with
        | Artist authorName -> Some authorName
        | _ -> None

    member x.TryGetWriter() =
        match x with
        | Writer authorName -> Some authorName
        | _ -> None

type Language =
    | English
    | Japanese

[<RequireQualifiedAccess>]
module Id =
    let value (Id v) = v
    let create = Id
    let fromGuid (v: Guid) = v.ToString() |> create

[<RequireQualifiedAccess>]
module ExternalId =
    let fromGuid = ExternalId.Guid
    let fromString = ExternalId.String

[<RequireQualifiedAccess>]
module Description =
    let value (Description v) = v
    let create = Description

[<RequireQualifiedAccess>]
module Cover =
    let value (Cover v) = v
    let create = Cover
    let fromString = Uri >> create
