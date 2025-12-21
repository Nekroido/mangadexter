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
| **FSharp.Core** | 6.0.5 | 10.x | Transitive (ships with SDK) | Low — likely bundled with net10.0 SDK |
| **FSharp.Data** | 4.2.9 | 4.2.9+ | Current | Low — stable API |
| **FSharp.Configuration** | 2.0 | 2.0 | Current | Low — no newer version |
| **AsyncSeq** | 1.12 | Latest | ⚠️ Outdated (2+ years) | Medium — may have API changes; verify batching/pagination logic in Pages/Manga.fs |
| **Spectre.Console** | 0.44 | 0.48+ | ⚠️ Outdated (1–2 years) | Low–Medium — check for deprecated UI APIs (MenuPrompt, SelectionPrompt) |
| **DotNetZip** | 1.16 | 1.16 (no updates) | ⚠️ Stale (2021) | Medium — no active maintenance; alternatives: SharpZipLib, System.IO.Compression.ZipFile |
| **FSharp.SystemTextJson** | 0.19.13 | Latest | Current | Low — minimal usage |
| **System.Text.Json** | 6.0.5 | 10.x | Outdated (transitive) | Low — will resolve via FSharp.Core upgrade |

---

## Steps

- [x] Audited all direct and transitive dependencies
- [x] Identified 3 high-risk packages (AsyncSeq, Spectre.Console, DotNetZip)
- [x] Confirmed net10.0 migration and Paket → NuGet complete (prerequisites)

## Dependencies

**Execution after**: Plans 0000 (.NET Migration) and 0001 (Paket → NuGet)

## Steps
- [ ] Update `Directory.Packages.props` — Bump AsyncSeq, Spectre.Console; replace DotNetZip with SharpZipLib; allow FSharp.Core to follow SDK
- [ ] Test AsyncSeq API compatibility
  - [ ] Run Pages/Manga.fs tests: focus on `fetchChapters` (batching patterns)
  - [ ] Regression test: download multi-chapter manga, verify ordering and completeness
- [ ] Test Spectre.Console compatibility
  - [ ] Verify Console.fs UI primitives (MenuPrompt, SelectionPrompt, Table, etc.)
  - [ ] Smoke test all Pages: Root menu, Search flow, Preferences display
- [ ] Replace DotNetZip 1.16 with SharpZipLib
  - [ ] Remove DotNetZip from Directory.Packages.props
  - [ ] Add SharpZipLib (latest stable) to Directory.Packages.props
  - [ ] Update File.fs: Change `using Ionic.Zip.ZipFile` → `using ICSharpCode.SharpZipLib.Zip.ZipFile`
  - [ ] Verify constructor and method calls compatible (UpdateEntry, Save, Comment)
- [ ] Build and regression test
  - [ ] `dotnet build src/App/App.fsproj`
  - [ ] `dotnet run --project src/App/App.fsproj` — full manual flow (search → download → archive)
- [ ] Update documentation (optional) — Update inline comments or copilot-instructions.md if API changes observed

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| AsyncSeq breaking API | Pages/Manga.fs fetch logic may fail | Run Pages/Manga batching tests manually; inspect AsyncSeq changelog before committing |
| Spectre.Console deprecations | UI prompts may show warnings or fail | Check build warnings; test all Console.fs UI modules |
| DotNetZip no maintenance | Security risk; may not work on net8.0 | Evaluate alternatives in parallel; keep as-is for now; flag for replacement in future |

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

