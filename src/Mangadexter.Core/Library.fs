namespace Mangadexter.Core

open System

type BookshelfId = Id
type BookshelfName = Title
type BookshelfItemId = Id
type Note = Note of string

type Library =
    { bookshelves: Bookshelf list }
    static member Zero = { bookshelves = [] }

and Bookshelf =
    { id: BookshelfId
      name: BookshelfName
      cover: Cover option
      createdAt: DateTimeOffset
      updatedAt: DateTimeOffset }

type BookshelfItem =
    { id: BookshelfItemId
      publication: Publication
      addedAt: DateTimeOffset
      note: Note option }

[<RequireQualifiedAccess>]
module Library =
    type Error =
        | BookshelfNotFound of BookshelfId
        | BookshelfWithNameAlreadyExists of BookshelfName
        | BookshelItemfAlreadyExists of BookshelfItem
        | InvalidCover of exn

    type AddBookshelfArgs = { name: string; coverPath: Uri option }

    type Command =
        | AddBookshelf of AddBookshelfArgs
        | RenameBookshelf of BookshelfId * name: string
        | DeleteBookshelf of BookshelfId
        | ChangeCover of BookshelfId * coverPath: string option
        | AddItem of BookshelfId * Publication
        | RemoveItem of BookshelfId * BookshelfItemId
        | UpdateNote of BookshelfId * BookshelfItemId * Note

    type Event =
        | BookshelfAdded of Bookshelf
        | BookshelfRenamed of Bookshelf
        | BookshelfDeleted of BookshelfId
        | CoverChanged of Bookshelf
        | ItemAdded of BookshelfId * BookshelfItem
        | ItemRemoved of BookshelfId * BookshelfItemId
        | NoteUpdated of BookshelfId * BookshelfItem

    type CommandHandler = Command -> Async<Result<Event, Error>>

    let apply state =
        function
        | BookshelfAdded bookshelf ->
            { state with
                bookshelves =
                    state.bookshelves
                    |> Seq.ofList
                    |> Seq.add bookshelf
                    |> List.ofSeq }
        | BookshelfRenamed bookshelf ->
            { state with
                bookshelves =
                    state.bookshelves
                    |> Seq.ofList
                    |> Seq.updateBy (fun x -> x.id = bookshelf.id) bookshelf
                    |> List.ofSeq }
        | BookshelfDeleted bookshelfId ->
            { state with
                bookshelves =
                    state.bookshelves
                    |> Seq.ofList
                    |> Seq.deleteBy (fun x -> x.id = bookshelfId)
                    |> List.ofSeq }
        | CoverChanged bookshelf ->
            { state with
                bookshelves =
                    state.bookshelves
                    |> List.filter (fun x -> x.id = bookshelf.id)
                    |> List.append [ bookshelf ] }
        | x -> failwithf "Todo: %A" x
