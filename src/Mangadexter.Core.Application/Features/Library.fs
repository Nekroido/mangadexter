module Library

open Mangadexter.Core
open System

type LibraryState = { bookshelves: Bookshelf list }

module LibraryState =
    let init _ = { bookshelves = [] }

    // Define a message type for the mailbox processor
    type BookshelfMessage =
        | LoadFromFile of Uri * AsyncReplyChannel<Result<Bookshelf list, Library.Error>>
        | Add of Bookshelf * AsyncReplyChannel<Result<Bookshelf, Library.Error>>

    // Create the mailbox processor
    let private bookshelfAgent =
        MailboxProcessor.Start (fun inbox ->
            let rec loop (state: LibraryState) =
                async {
                    // Receive a message
                    let! msg = inbox.Receive()

                    match msg with
                    | LoadFromFile (uri, reply) ->
                        reply.Reply(Ok [])
                        return! loop { state with bookshelves = [] }
                    | Add (bookshelf, reply) ->
                        // Check if a bookshelf with the same title already exists
                        match state.bookshelves
                              |> List.tryFind (fun b -> b.name = bookshelf.name)
                            with
                        | Some _ ->
                            // If it does, reply with an error
                            reply.Reply(Error(Library.Error.BookshelfWithNameAlreadyExists bookshelf.name))
                            return! loop state
                        | None ->
                            // If it doesn't, add the new bookshelf and reply with success
                            reply.Reply(Ok bookshelf)
                            return! loop { state with bookshelves = bookshelf :: state.bookshelves }
                }

            // Start the loop with an empty list of bookshelves
            loop <| init ())

    let loadFromFile (file: Uri) : Async<Result<Bookshelf list, Library.Error>> =
        bookshelfAgent.PostAndAsyncReply(fun reply -> LoadFromFile(file, reply))

    // Update the AddBookshelf command to use the mailbox processor
    let addBookshelf (bookshelf: Bookshelf) : Async<Result<Bookshelf, Library.Error>> =
        bookshelfAgent.PostAndAsyncReply(fun reply -> Add(bookshelf, reply))
