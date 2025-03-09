module Configuration

open Mangadexter.Core
open System

type ConfigurationState = { configuration: Configuration }

module ConfigurationState =
    let init _ =
        { configuration = Configuration.defaults }

    // Define a message type for the mailbox processor
    type BookshelfMessage = LoadFromFile of Uri * AsyncReplyChannel<Result<Configuration, Configuration.Error>>

    // Create the mailbox processor
    let private configurationAgent =
        MailboxProcessor.Start (fun inbox ->
            let rec loop (state: ConfigurationState) =
                async {
                    // Receive a message
                    let! msg = inbox.Receive()

                    match msg with
                    | LoadFromFile (uri, reply) ->
                        let configuration = Configuration.defaults
                        reply.Reply(Ok configuration)
                        return! loop { state with configuration = configuration }
                }

            // Start the loop with an empty list of bookshelves
            loop <| init ())

    let loadFromFile (file: Uri) : Async<Result<Configuration, Configuration.Error>> =
        configurationAgent.PostAndAsyncReply(fun reply -> LoadFromFile(file, reply))
