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
| **Doc comments** | ❌ None (6 comment lines total, mostly inline comments) |
| **Analyzer rules** | ❌ None enabled in .fsproj |
| **Style guide** | ❌ No formal document; conventions inferred from code |
| **TODO** | One found in File.fs: "delete existing file?" — indicates pending cleanup logic |

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
- [ ] Create `docs/STYLE_GUIDE.md`
  - [ ] Document naming conventions (PascalCase types, camelCase locals, UPPERCASE constants)
  - [ ] Document module organization (single responsibility, [RequireQualifiedAccess])
  - [ ] Document error handling pattern (Result<'T, string>, no exceptions)
  - [ ] Document async patterns (async { }, AsyncSeq for streaming)
  - [ ] Document type annotations and comment style
- [ ] Add XML doc comments to public APIs
  - [ ] Target: Data.fs, Manga.fs, Pages/*.fs (public functions, types, modules)
  - [ ] Format: Standard F# `/// <summary>...</summary>`
- [ ] Configure F# analyzer rules (non-optional)
  - [ ] Add FSharp.Analyzers.SDK to Directory.Packages.props
  - [ ] Add `<Analyzer>` items to App.fsproj
  - [ ] Enable rules: unused imports, naming conventions, line length consistency
- [ ] Resolve existing TODO in File.fs — Clarify CBZ overwrite behavior
- [ ] Review and merge
  - [ ] Run `dotnet build` (verify no analyzer regressions)
  - [ ] Peer review STYLE_GUIDE.md
  - [ ] Update copilot-instructions.md cross-reference

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| No established code review process | Style guide not enforced | Use analyzer rules in CI (future plan) to catch violations; document as aspirational for now |
| XML doc comments are verbose | May slow initial adoption | Start with public-facing modules (Data, Manga, Pages); expand incrementally |

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

