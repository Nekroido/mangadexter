namespace Utils

open System
open System.IO
open Microsoft.FSharp.Reflection

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

[<RequireQualifiedAccess>]
module Directory =
    let createForPath (path: string) =
        path
        |> Path.GetDirectoryName
        |> Directory.CreateDirectory
        |> ignore

[<RequireQualifiedAccess>]
module Path =
    let combine (parts: string seq) = parts |> Array.ofSeq |> Path.Combine

    let getFileExtension (path: string) = path |> Path.GetExtension

    let toSafePath (path: string) =
        String.Join("_", Path.GetInvalidFileNameChars() |> path.Split)

[<RequireQualifiedAccess>]
module Result =
    let isOk result =
        match result with
        | Result.Ok _ -> true
        | _ -> false

    let isError result = result |> isOk |> not

    let proceedIfOk result =
        match result with
        | Result.Ok r -> r
        | Result.Error ex -> failwith ex

[<RequireQualifiedAccess>]
module DiscriminatedUnion =
    let createCase (caseInfo: UnionCaseInfo) =
        FSharpValue.MakeUnion(caseInfo, Array.zeroCreate (caseInfo.GetFields().Length))

    let listCases<'a> () =
        typeof<'a>
        |> FSharpType.GetUnionCases
        |> Seq.map (fun caseInfo -> caseInfo |> createCase :?> 'a)

[<RequireQualifiedAccess;AutoOpen>]
module String =
    let join separator parts =
        parts
        |> Seq.filter (fun x -> String.IsNullOrWhiteSpace(x) = false)
        |> String.concat separator

    let inline stringf format (x : ^a) = 
        (^a : (member ToString : string -> string) (x, format))
