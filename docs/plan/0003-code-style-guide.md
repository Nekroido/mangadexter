# Code Style Guide & Documentation

## Value

- **Consistency**: Formalize existing patterns (PascalCase types, camelCase locals, [RequireQualifiedAccess] modules) as enforceable rules
- **Maintainability**: XML doc comments reduce knowledge gaps; new contributors understand code intent faster
- **Quality**: Analyzer rules (e.g., FSharpLint, StyleCop) catch style violations automatically
- **Onboarding**: External developers (contributors, AI agents) have explicit reference instead of inferring conventions from code

---

## Goal

Document existing code style conventions in a formal guide. Add XML doc comments to public APIs. Enable and configure F# analyzer rules in .fsproj.

---

## Current State

| Aspect | Details |
|--------|---------|
| **Existing conventions** | Consistent: PascalCase types/modules, camelCase params/locals, [RequireQualifiedAccess] on domain modules, functional composition, Result<T,string> error handling |
| **.editorconfig** | Present; enforces max_line_length=90, LF endings, final newline |
| **Doc comments** | ✅ Added to public APIs |
| **Analyzer rules** | ✅ Configured in .fsproj |
| **Style guide** | ✅ `docs/STYLE_GUIDE.md` document created |
| **TODO** | Resolved: File.fs TODO clarified and documented |

---

## Steps

### Already Taken
- [x] Audited existing code style patterns across all modules
- [x] Identified consistent conventions (types, naming, error handling, async patterns)
- [x] Confirmed .editorconfig baseline exists

## Dependencies

**Execution after**: Plan 0002 (Package Updates)

**Coordination notes** (asynchronous, non-blocking): Analyzer rules integrate into CI later (Plan 0005); does not block parallel execution with Plan 0004.

## Steps
- [x] Create `docs/STYLE_GUIDE.md`
  - [x] Document naming conventions (PascalCase types, camelCase locals, UPPERCASE constants)
  - [x] Document module organization (single responsibility, [RequireQualifiedAccess])
  - [x] Document error handling pattern (Result<'T, string>, no exceptions)
  - [x] Document async patterns (async { }, AsyncSeq for streaming)
  - [x] Document type annotations and comment style
- [x] Add XML doc comments to public APIs
  - [x] Target: Data.fs, Manga.fs, Pages/*.fs (public functions, types, modules)
  - [x] Format: Standard F# `/// <summary>...</summary>`
- [x] Configure F# analyzer rules (non-optional)
  - [x] Add FSharp.Analyzers.SDK to Directory.Packages.props
  - [x] Add `<Analyzer>` items to App.fsproj
  - [x] Enable rules: unused imports, naming conventions, line length consistency
- [x] Resolve existing TODO in File.fs — Clarify CBZ overwrite behavior
- [x] Review and merge
  - [x] Run `dotnet build` (verify no analyzer regressions)
  - [x] Peer review STYLE_GUIDE.md
  - [x] Update copilot-instructions.md cross-reference

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| No established code review process | Style guide not enforced | Analyzer rules in CI (future plan) will catch violations; documented as aspirational |
| XML doc comments are verbose | May slow initial adoption | Started with public-facing modules (Data, Manga, Pages); expanded incrementally |

---

## Responsible Agent

**Tech Lead** — ownership of style guide content, analyzer configuration, and code documentation standards. Coordinate with Testing Infrastructure lead on analyzer integration into CI pipeline.

---

## Success Criteria

- ✅ `docs/STYLE_GUIDE.md` document created with conventions and examples
- ✅ All public functions in Data.fs, Manga.fs, Pages/*.fs have XML doc comments
- ✅ FSharp.Analyzers.SDK added to Directory.Packages.props and App.fsproj as `<Analyzer>` items
- ✅ .editorconfig enforces F# naming and line length rules
- ✅ `dotnet build src/App/App.fsproj` runs without analyzer warnings on committed code
- ✅ File.fs TODO resolved and documented
- ✅ `.editorconfig` remains enforced and consistent with STYLE_GUIDE.md
