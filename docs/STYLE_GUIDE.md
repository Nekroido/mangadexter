# Mangadexter Code Style Guide

This document formalizes the code style conventions used throughout the Mangadexter F# project. All contributors should follow these guidelines for consistency and maintainability.

---

## Table of Contents

1. [Naming Conventions](#naming-conventions)
2. [Module Organization](#module-organization)
3. [Error Handling](#error-handling)
4. [Async Patterns](#async-patterns)
5. [Documentation](#documentation)
6. [Editor Configuration](#editor-configuration)

---

## Naming Conventions

### Types and Type Aliases

- **Pascal Case**: All type definitions, records, discriminated unions, and aliases use `PascalCase`
- **Qualified Names**: Types marked with `[<RequireQualifiedAccess>]` are accessed with module qualification (e.g., `Action.SearchManga`)

```fsharp
// ✓ Good
type Manga = MangaList.Datum
type Chapter = ChapterList.Datum

[<RequireQualifiedAccess>]
type Quality =
    | High
    | Low
```

### Functions and Values

- **Camel Case**: All function and value names use `camelCase`
- **Descriptive**: Function names are verb-centric where applicable (e.g., `fetchChapters`, `mapChapterMetadata`)
- **Private Scope**: Internal helper functions prefixed with `private` keyword; no leading underscore convention

```fsharp
// ✓ Good
let searchManga (query: string) = ...
let private mapCredits (entity: Relationship) = ...

// ✗ Avoid
let _fetchData () = ...
```

### Constants

- **UPPER_SNAKE_CASE**: Literal constants and configuration values use `UPPER_SNAKE_CASE` within types; simple literals use `camelCase`

```fsharp
// ✓ Good
[<Literal>]
let BaseUrl = "https://api.mangadex.org/"

let defaultTimeout = 30000
```

### Parameters and Locals

- **Camel Case**: All parameters and local variables use `camelCase`
- **Type-Hinted**: Explicit type annotations on function parameters; inferred types acceptable for locals

```fsharp
// ✓ Good
let fetchPages (mangaId: string) (chapterId: string) =
    let preferredLanguage = "en"
    ...
```

---

## Module Organization

### File Structure

- **Single Responsibility**: Each `.fs` file contains one primary module
- **Logical Nesting**: Submodules use `Module.Submodule` nesting only when necessary for type grouping
- **Compile Order**: Files are ordered in `.fsproj` by dependency (bottom-up): utilities → core logic → pages
- **Visibility**: Public functions listed first; private helpers below

```fsharp
module Data

// Public functions first
let searchManga limit offset language query = ...
let getTitle (manga: Manga) = ...

// Private helpers last
let private buildUrl endpoint params = ...
```

### Module Qualification

- **RequireQualifiedAccess**: Used on domain types and action enums to force explicit qualification
- **Transparent Modules**: Used for pure utility functions and helpers (no qualification required)

```fsharp
// ✓ Good (requires qualification)
[<RequireQualifiedAccess>]
type Action =
    | Search
    | Exit

// ✓ Good (transparent utility)
[<RequireQualifiedAccess>]
module Path =
    let combine (parts: string seq) = ...

// Usage
let action = Action.Search
let path = Path.combine ["src"; "App"]
```

---

## Error Handling

### Result Type Pattern

- **Result<'T, string>**: All operations returning errors use `Result<'T, string>` for type safety
- **No Exceptions**: Exceptions reserved for unrecoverable conditions; prefer `Result` for expected failures
- **Error Messages**: Error strings are descriptive and actionable

```fsharp
// ✓ Good
let fetchChapters (manga: Manga) : Async<Result<Chapter list, string>> =
    async {
        try
            let! chapters = ChapterList.AsyncLoad url
            return Ok (chapters |> List.ofSeq)
        with
        | ex -> return Error $"Failed to fetch chapters: {ex.Message}"
    }

// ✗ Avoid (using exceptions for control flow)
let fetchChapters (manga: Manga) =
    try
        ChapterList.AsyncLoad url |> Async.RunSynchronously
    with _ -> failwith "Network error"
```

### Chaining Results

- **proceedIfOk**: Use helper function to chain operations and propagate errors
- **Pattern Matching**: Explicit match on `Ok | Error` only when handling specific errors

```fsharp
// ✓ Good
result
|> Result.proceedIfOk
|> selectChapters
|> downloadChapters manga

// ✓ Good (when specific handling needed)
match searchMangaResult with
| Ok manga -> Manga.initialize manga
| Error msg -> Console.echo $"Search failed: {msg}"
```

---

## Async Patterns

### Async Workflows

- **async { }**: All async operations use F# async workflows
- **Async.Sequential**: Sequential I/O operations chained with `Async.Sequential`
- **AsyncSeq**: Streaming/batching operations use `AsyncSeq` for lazy evaluation

```fsharp
// ✓ Good (single async)
let fetchManga (id: string) =
    async {
        let! response = Http.AsyncRequestStream url
        return response.ResponseStream
    }

// ✓ Good (sequential chain)
let results = 
    pages
    |> Seq.map fetchPage
    |> Async.Sequential

// ✓ Good (lazy batching)
AsyncSeq.unfoldAsync batchChapters 0
|> AsyncSeq.concatSeq
|> AsyncSeq.toArrayAsync
```

### Avoiding Synchronous Blocking

- **Never use Async.RunSynchronously** in Page modules or core logic; only in Program.fs entrypoint
- **Preserve async chains**: All intermediate operations remain async

```fsharp
// ✓ Good
Root.initialize () |> Async.RunSynchronously  // Only in Program.fs

// ✗ Avoid
let chapters = 
    fetchChapters manga
    |> Async.RunSynchronously  // Blocks thread; use within async workflow instead
```

---

## Documentation

### XML Doc Comments

- **Public APIs**: All public functions, types, and modules must have XML doc comments
- **Format**: Standard F# `/// <summary>...</summary>` format
- **Scope**: Focus on public-facing modules (Data, Manga, Pages) and domain types
- **Brevity**: Keep comments concise; one summary + example for complex functions

```fsharp
/// <summary>
/// Search for manga by title on Mangadex.
/// </summary>
/// <param name="limit">Maximum results to return (default: 30)</param>
/// <param name="offset">Offset for pagination (default: 0)</param>
/// <param name="language">Preferred language (English or Japanese)</param>
/// <param name="query">Search query string</param>
/// <returns>Async result containing manga list or error message</returns>
let searchManga (limit: int) (offset: int) (language: Language) (query: string) : Async<Result<Manga list, string>> =
    ...
```

### Inline Comments

- **Sparse**: Use only for non-obvious logic; code should be self-documenting
- **Why, not What**: Explain decisions, not syntax

```fsharp
// ✓ Good (explains decision)
// Prioritize latest chapters to handle reuploads
let chapters = 
    results
    |> Seq.sortByDescending getPublishDate
    |> Seq.distinctBy getFormattedChapterNumber

// ✗ Avoid (obvious from code)
let title = manga |> getTitle  // Get the title
```

### Localized Strings

- **Strings.resx**: All user-facing text (prompts, labels, messages) must go in `Strings.resx`
- **Accessor Pattern**: Access via `Strings.Strings.GetString "keyName"`
- **No Hard-Coded Literals**: Avoid inline string literals in Pages and Console modules

```fsharp
// ✓ Good
"Please select an action:" |> Console.ask  // Consider moving to Strings.resx

// ✗ Avoid
Console.echo "Menu: (1) Search (2) Exit"  // Hard-coded string

// Better
let action = Strings.Strings.GetString "Pages.Root.SelectAction"
Console.echo action
```

---

## Editor Configuration

### .editorconfig Rules

The project enforces the following rules via `.editorconfig`:

- **Line Length**: Maximum 90 characters (enforced)
- **Indentation**: 4 spaces (enforced)
- **Line Endings**: LF (enforced)
- **Final Newline**: Required (enforced)

**Example .editorconfig entry**:
```ini
[*.fs]
max_line_length = 90
indent_size = 4
end_of_line = lf
insert_final_newline = true
```

### Analyzer Rules

The following F# analyzers are enabled:

- **Unused Imports**: Removed via refactoring
- **Naming Conventions**: Enforced through code review
- **Line Length**: Warnings at 90+ characters

Run `dotnet build` to check compliance; fix with refactoring commands (see [copilot-instructions.md](../.github/copilot-instructions.md)).

---

## TODO and Known Issues

### Pending Cleanup

- **File.fs (Line 39)**: Remove old CBZ overwrite logic comment (`// todo: delete existing file?`) — now handled via explicit `File.Delete`
- **Metadata.xml**: Consider whether metadata should be embedded in CBZ comments or as a separate entry

---

## References

- **F# Naming Conventions**: [Official F# Style Guide](https://docs.microsoft.com/en-us/dotnet/fsharp/style-guide/)
- **Error Handling Patterns**: See [copilot-instructions.md - Conventions & Patterns](../.github/copilot-instructions.md)
- **Async Best Practices**: See [copilot-instructions.md - Key Modules](../.github/copilot-instructions.md)
