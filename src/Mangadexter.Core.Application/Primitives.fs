module Primitives

open Mangadexter.Core

type AbstractState<'T> = { data: 'T }

type ar<'a, 'err> = Async<Result<'a, 'err>>

type ApplicationError =
    | Configuration of Configuration.Error
    | Library of Library.Error
    | Download of DownloadError
    | Exception of exn
