# Package Version Updates

## Value

- **Security**: Patch outdated libraries with known vulnerabilities (DotNetZip last updated 2021, AsyncSeq ~2020)
- **Compatibility**: Align AsyncSeq, Spectre.Console, System.Text.Json with net10.0 expectations
- **Performance**: Newer versions include bug fixes and optimizations
- **Stability**: Unblock Testing Infrastructure plan (requires latest stable versions for test framework integration)

---

## Goal

Bump versions of outdated packages to current stable releases. Verify no breaking API changes and full feature parity.

---

## Current State

| Package | Current | Latest | Status | Risk |
|---------|---------|--------|--------|------|
| **FSharp.Core** | 10.x | 10.x | Updated | Low — bundled with SDK |
| **FSharp.Data** | 4.2.9 | 4.2.9+ | Current | Low — stable API |
| **FSharp.Configuration** | 2.0 | 2.0 | Current | Low — no newer version |
| **AsyncSeq** | 1.12.0 | Latest | Updated | Low — no API changes detected |
| **Spectre.Console** | 0.49.1 | 0.49.1+ | Updated | Low — no UI issues detected |
| **DotNetZip** | Removed | 1.16 (no updates) | Replaced | Medium — replaced with SharpZipLib |
| **FSharp.SystemTextJson** | 0.19.13 | Latest | Current | Low — minimal usage |
| **System.Text.Json** | 10.x | 10.x | Updated | Low — via FSharp.Core upgrade |

---

## Steps

- [x] Audited all direct and transitive dependencies
- [x] Identified 3 high-risk packages (AsyncSeq, Spectre.Console, DotNetZip)
- [x] Confirmed net10.0 migration and Paket → NuGet complete (prerequisites)

## Dependencies

**Execution after**: Plans 0000 (.NET Migration) and 0001 (Paket → NuGet)

## Steps
- [x] Update `Directory.Packages.props` — Bump AsyncSeq, Spectre.Console; replace DotNetZip with SharpZipLib; allow FSharp.Core to follow SDK
- [x] Test AsyncSeq API compatibility
  - [x] Run Pages/Manga.fs tests: focus on `fetchChapters` (batching patterns)
  - [x] Regression test: download multi-chapter manga, verify ordering and completeness
- [x] Test Spectre.Console compatibility
  - [x] Verify Console.fs UI primitives (MenuPrompt, SelectionPrompt, Table, etc.)
  - [x] Smoke test all Pages: Root menu, Search flow, Preferences display
- [x] Replace DotNetZip 1.16 with SharpZipLib
  - [x] Remove DotNetZip from Directory.Packages.props
  - [x] Add SharpZipLib (latest stable) to Directory.Packages.props
  - [x] Update File.fs: Change `using Ionic.Zip.ZipFile` → `using ICSharpCode.SharpZipLib.Zip.ZipFile`
  - [x] Verify constructor and method calls compatible (UpdateEntry, Save, Comment)
- [x] Build and regression test
  - [x] `dotnet build src/App/App.fsproj`
  - [x] `dotnet run --project src/App/App.fsproj` — full manual flow (search → download → archive)
- [x] Update documentation (optional) — Update inline comments or copilot-instructions.md if API changes observed

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| AsyncSeq breaking API | Pages/Manga.fs fetch logic may fail | Resolved by testing and confirming no API changes |
| Spectre.Console deprecations | UI prompts may show warnings or fail | Resolved by testing all UI modules |
| DotNetZip no maintenance | Security risk; may not work on net8.0 | Resolved by replacing with SharpZipLib |

---

## Responsible Agent

**Maintainer** — ownership of dependency versions, transitive compatibility, and regression testing. Coordinate with Testing Infrastructure lead on test coverage for updated APIs.

---

## Success Criteria

- ✅ `Directory.Packages.props` updated with latest stable versions
- ✅ `dotnet build` succeeds with no warnings related to outdated packages
- ✅ AsyncSeq batching and pagination work correctly (Pages/Manga.fs tested manually)
- ✅ Spectre.Console UI prompts render without deprecation warnings
- ✅ DotNetZip replaced with SharpZipLib; no Ionic.Zip references remain in codebase
- ✅ Full manual flow (search → download → CBZ) executes without errors
- ✅ Security audit: SharpZipLib has no known HIGH-severity vulnerabilities
- ✅ No silent API breaking changes (confirmed via regression testing)
