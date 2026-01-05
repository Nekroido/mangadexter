# Copilot / AI Agent Instructions for Mangadexter

## Overview

This document provides essential, actionable knowledge for AI coding agents to be immediately productive in this **F# console application + multi-project repository**.

- **Primary focus**: `src/App/` (console UI using Mangadex API)
- **Tech stack**: .NET 10 (net10.0), F# language, NuGet for dependency management
- **Architecture**: Modular console app with async streaming for downloads and CBZ archiving
- **Roadmap**: See [docs/todo.md](../docs/todo.md) for migration plans and project roadmap; plans use checkbox format for compact diffs and checklist-style tracking

## Project Structure

```
src/App/
├── Program.fs              # Entrypoint → Pages.Root.initialize
├── Pages/                  # Page modules (Root, Search, Manga, Preferences)
├── Console.fs              # CLI UI primitives (MenuPrompt, MultiSelectionPrompt, SelectionPrompt, TextPrompt, Table)
├── Http.fs                 # Network I/O (FSharp.Data.AsyncRequestStream)
├── Data.fs                 # Mangadex API URLs + JsonProviders
├── Manga.fs                # Core domain logic (Manga search, formatting)
├── Chapter.fs              # Chapter fetching + language/quality selection
├── File.fs                 # CBZ archiving helpers
├── Preferences.fs          # Settings persistence + config types (Language, Quality)
├── Utils.fs                # UriBuilder, String helpers
├── Strings.fs              # Localized string accessors (from Strings.resx)
├── Strings.resx            # Localized UI strings (editable resource file)
├── chapters-sample.json    # Sample JSON for JsonProvider type inference
├── preferences.yaml        # Default preferences (embedded)
└── test*.fsx               # Manual test scripts (not run by default)
```

Other projects (`Mangadexter.Core*`, `Mangadexter.Gui*`) are in the solution but not primary for CLI work.

## Data Flow & Architecture

**Page flow**: `Program.fs` → `Pages.Root.initialize` → `Pages.Search` / `Pages.Manga` / `Pages.Preferences`

**Core pipeline** (search → download → archive):
1. User searches via `Pages.Search` → calls `Data.searchManga`
2. Select manga → `Pages.Manga.initialize` fetches chapters via `Data.Chapter.listChapters`
3. Chapters grouped by volume and filtered via `Pages.Manga.selectChapters`
4. Each chapter's pages download via `Http.fetchFile`, then packaged into CBZ by `File.cbzBuilder`

**Key modules** (in dependency order):
- `Strings.fs`: Resource-based localized string accessors; update `Strings.resx` for new UI text.
- `Utils.fs`: `UriBuilder` for API URL construction, string/path helpers.
- `Preferences.fs`: Persists settings to `preferences.yaml`; defines `Language` enum (English, Japanese) and `Quality` enum (High, Low) for chapter downloads.
- `Data.fs`: Builds Mangadex API URLs, defines JsonProvider types for typed JSON parsing (MangaList, ChapterList, ChapterServer).
- `Http.fs`: Uses `FSharp.Data.Http.AsyncRequestStream`, returns `Result` with stream.
- `Chapter.fs`: Fetches chapters via `Data.ChapterList.AsyncLoad`; respects language and quality preferences.
- `Manga.fs`: Orchestrates manga search, chapter batching (`AsyncSeq` patterns), UI selection, page downloads, and CBZ creation.
- `File.fs`: CBZ builder API; combines pages and metadata into `.cbz` archives.
- `Console.fs`: Exports UI primitives (MenuPrompt, MultiSelectionPrompt, SelectionPrompt, TextPrompt, Table, status/live feedback). All used by Pages.

## Conventions & Patterns

**Module organization**: Single-file modules, small scope. Add helpers in same module unless cross-cutting; prefer modular design.

**Compilation order**: F# requires top-down file ordering in `.fsproj`. `App.fsproj` lists files in dependency order—edit carefully or compilation will fail. Helper modules (Utils, Console) come first; Pages come last.

**UI patterns** (all live in `Console.fs` as nested modules):
```fsharp
// Single-choice selection
MenuPrompt.create<Action> "Pick one:" cases Action.toString
|> Console.prompt

// Single-choice with search (like Spectre's SelectionPrompt)
SelectionPrompt.create<Manga> "Select manga:"
|> SelectionPrompt.addChoices items
|> SelectionPrompt.withConverter (Func<_, string>(toString))
|> Console.prompt

// Multi-choice selection
MultiSelectionPrompt.create<string> "Select items:"
|> MultiSelectionPrompt.addChoiceGroup groupName choices
|> Console.prompt

// Text input
TextPrompt.create<string> "Enter text:" defaultValue
|> Console.prompt

// Table display
Table.create ["col1"; "col2"] |> Table.addRow [...] |> Console.render

// Async operation with live status
Console.live "status" async_work
```

**Actions as discriminated unions** (see `Pages/Root.fs`, `Pages/Manga.fs`):
```fsharp
[<RequireQualifiedAccess>]
type Action =
    | SearchManga
    | Preferences
    | Exit
    static member toString x = match x with ...
    static member fromString x = match x with ...

// Discover actions:
DiscriminatedUnion.listCases<Action>()
```

**Async & streaming** (network/file I/O):
- Use `async { }` blocks and `Async.Sequential` for sequential IO
- Use `AsyncSeq` (from `FSharpx.Control`) for batching/pagination (see `Pages.Manga.fetchChapters`)
- Preserve async chains; avoid converting to synchronous

**Error handling**:
- Most functions return `Result<'T, string>` 
- Use `Result.proceedIfOk` to chain operations
- Never convert to exceptions unless purposeful—preserves `Result` semantics

**Embedded resources** (update `App.fsproj` if adding):
- `Strings.resx`: UI strings (localized); access via `Strings.Strings.GetString "key"`
- `chapters-sample.json`: Sample JSON for JsonProvider type checking; update after API schema changes
- `preferences.yaml`: Default preferences (loaded on app startup)

**Configuration types** (in `Preferences.fs`):
- `Language`: English | Japanese — affects chapter language filter
- `Quality`: High | Low — selects chapter page resolution

## Build / Run / Debug

**Restore NuGet dependencies** (Windows, from repo root):
```powershell
dotnet restore
```

**Build the console app**:
```bash
dotnet build src/App/App.fsproj
```

**Run the console app**:
```bash
dotnet run --project src/App/App.fsproj
```

**VS Code**: Use workspace task `Build: App.fsproj`

**Notes on NuGet & project imports**:
- `src/App/App.fsproj` uses Directory.Packages.props for central package management
- If you modify package versions, run restore before building

## External Dependencies & Integration Points

- **Network parsing and requests**: `FSharp.Data` (JsonProvider, Http). Sample/URL constants are in `Data.fs`.
- **Async sequences**: `FSharpx.Control` and `AsyncSeq` patterns appear in `Pages/Manga.fs`.
- **Archiving + file IO**: `File.fs` contains CBZ builder helpers; follow its API for creating output files.

## External Libraries & APIs

| Library | Purpose | Version | Docs |
|---------|---------|---------|------|
| **FSharp.Data** | JSON parsing (JsonProvider) + HTTP requests | 4.2.9 | [FSharp.Data docs](https://fsprojects.github.io/FSharp.Data/) |
| **AsyncSeq** | Async sequence operations (batching, pagination) | Latest | [AsyncSeq GitHub](https://github.com/fsprojects/AsyncSeq) |
| **Spectre.Console** | Rich console output (colors, tables, prompts) | 0.49.1 | [Spectre.Console docs](https://spectreconsole.net/) |
| **DotNetZip** | CBZ archive creation (Zip file manipulation) | 1.4.2 | [DotNetZip docs](https://codekicker.de/dotnetzip/) |
| **FSharp.Configuration** | YAML config parsing (preferences loading) | Latest | [FSharp.Configuration](https://fsprojects.github.io/FSharp.Configuration/) |
| **FSharp.SystemTextJson** | JSON serialization (alternative to Newtonsoft) | Latest | [FSharp.SystemTextJson](https://github.com/Tarmil/FSharp.SystemTextJson) |

### Mangadex API

- **Base URL**: `https://api.mangadex.org/`
- **Main endpoints**:
  - `GET /manga` — Search and list manga with filters
  - `GET /manga/{id}/feed` — Fetch chapter list for a manga
  - `GET /at-home/server/{chapterId}` — Get chapter page server URLs
- **Documentation**: [Mangadex API Docs](https://api.mangadex.org/docs/)
- **Rate limits**: Check API docs for current limits; handle `429` (Too Many Requests) gracefully
- **Authentication**: Public API; no token required for read operations

### .NET Runtime & Language

- **.NET 10.0** (`net10.0` TFM): Core runtime; LTS version
- **F# Language**: Functional-first, uses type providers, async workflows, discriminated unions
- **FSharp.Core**: Standard library shipped with .NET SDK

### Key Integration Pattern

When working with external APIs or libraries:
1. **Sample data first**: Add sample responses to repo (e.g., `chapters-sample.json`) for JsonProvider type inference
2. **Result handling**: Wrap async API calls in `Result<'T, string>` to propagate errors cleanly
3. **Stream-based I/O**: Use `AsyncRequestStream` and streams (not buffering entire responses) for large files
4. **Configuration**: Preferences and settings are loaded at startup from `preferences.yaml`

## Common Patterns by Task

### Add a new prompt/menu
Mirror the pattern in `Pages/Root.fs`:
```fsharp
[<RequireQualifiedAccess>]
type Action =
    | Option1
    | Option2
    
    static member toString x =
        match x with
        | Action.Option1 -> "Option 1"
        | Action.Option2 -> "Option 2"
    
    static member fromString x =
        match x with
        | "Option 1" -> Action.Option1
        | "Option 2" -> Action.Option2
        | _ -> failwith $"Unknown action {x}"

let showMenu () =
    MenuPrompt.create<Action>
        "Choose an option:"
        (DiscriminatedUnion.listCases<Action> ())
        Action.toString
    |> Console.prompt
```

### Add a new API call
Follow the pattern in `Data.fs`:
1. Define a sample URL or use a local JSON file (like `chapters-sample.json`)
2. Create a JsonProvider type:
```fsharp
let SampleUrl = BaseUrl + "endpoint?param=value"
type MyData = JsonProvider<SampleUrl>

let fetchMyData id =
    async {
        let url = makeRequestUrl "endpoint" [("id", id)]
        let! result = url |> Http.AsyncRequestStream
        // parse result and return Result<'T, string>
    }
```

### Fix download logic
Inspect `Pages/Manga.fs` (functions `fetchChapters`, `downloadChapters`) and `Http.fetchFile` for streaming behavior. Key pattern:
```fsharp
let! downloadResult = Http.fetchFile url outputStream
match downloadResult with
| Ok stream -> // process stream
| Error msg -> // handle error
```

## Gotchas & Important Notes

- If you change a JSON schema or API endpoint, update `chapters-sample.json` and the `JsonProvider` usages in `Data.fs` and rebuild.
- Keep UI text in `Strings.resx` (localized strings) when changing prompts.
- Preserve existing `Result` and `async` flows; converting a flow to synchronous or throwing exceptions can break higher-level orchestration.

## Pre-PR Checklist

- Run `dotnet restore` and `dotnet build src/App/App.fsproj` locally
- Run the console app with `dotnet run --project src/App/App.fsproj` to manually exercise flows you changed
- Update `chapters-sample.json` when altering providers or response parsing
- If adding new UI text, update `Strings.resx` and regenerate `Strings.fs`
- If changing module dependencies, verify `.fsproj` compile order is correct
- For new features, ask for a short description and I'll point to exact modules to edit

## Testing

- Manual test scripts exist in `test.fsx`, `test2.fsx`, `test3.fsx` — use these for ad-hoc validation
- xUnit test suite with 14 passing tests; automated test infrastructure operational
