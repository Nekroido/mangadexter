namespace Mangadexter.Core

open System
open System.IO

[<RequireQualifiedAccess>]
module UriBuilder =
    let fromString (url: string) = UriBuilder(url)

    let setPath path (uriBuilder: UriBuilder) =
        uriBuilder.Path <- path
        uriBuilder

    let setQuery query (uriBuilder: UriBuilder) =
        uriBuilder.Query <- query
        uriBuilder

    let toString (uriBuilder: UriBuilder) = uriBuilder.ToString()

    let toUri (uriBuilder: UriBuilder) = uriBuilder.Uri

[<RequireQualifiedAccess>]
module Path =
    let combine (parts: string seq) = parts |> Array.ofSeq |> Path.Combine

    let getFileExtension (path: string) = path |> Path.GetExtension

    let toSafePath (path: string) =
        String.Join("_", Path.GetInvalidFileNameChars() |> path.Split)

[<RequireQualifiedAccess; AutoOpen>]
module String =
    let inline stringf format (x: ^a) =
        (^a: (member ToString: string -> string) (x, format))

[<RequireQualifiedAccess>]
module Seq =
    let deleteBy<'a> = Seq.filter<'a>

    let add<'a> = Seq.singleton<'a> >> Seq.append<'a>

    let updateBy<'a> (matchesItem: 'a -> bool) (item: 'a) =
        Seq.map<'a, 'a> (fun x -> if matchesItem x then item else x)
