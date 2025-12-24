module Pages.Root

open Console
open Utils

[<RequireQualifiedAccess>]
type Action =
    | SearchManga
    | Preferences
    | Exit

    static member toString x =
        match x with
        | Action.SearchManga -> Strings.Strings.GetString "Pages.Root.Actions.SearchManga"
        | Action.Preferences -> Strings.Strings.GetString "Pages.Root.Actions.Preferences"
        | Action.Exit -> Strings.Strings.GetString "Pages.Root.Actions.Exit"

    static member fromString x =
        match x with
        | s when s = Strings.Strings.GetString "Pages.Root.Actions.SearchManga" -> Action.SearchManga
        | s when s = Strings.Strings.GetString "Pages.Root.Actions.Preferences" -> Action.Preferences
        | s when s = Strings.Strings.GetString "Pages.Root.Actions.Exit" -> Action.Exit
        | _ -> failwith $"Unknown action {x}"

let showActions () =
    Console.clear ()

    MenuPrompt.create<Action>
        (Strings.Strings.GetString "Pages.Root.SelectAction")
        (DiscriminatedUnion.listCases<Action> ())
        Action.toString
    |> Console.prompt

let rec handleAction action =
    action
    |> function
        | Action.SearchManga -> Search.initialize initialize
        | Action.Preferences -> Preferences.initialize initialize
        | Action.Exit -> Strings.Strings.GetString "Pages.Root.Messages.Exiting" |> Console.echo

and initialize = showActions >> handleAction
