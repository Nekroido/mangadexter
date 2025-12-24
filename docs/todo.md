# Mangadexter Project Roadmap

Master todo for Mangadexter F# console application. Six linked work streams covering framework modernization, dependency management, code quality, testing, and documentation.

---

## Overview

| Work Stream | Status | Priority | Effort | Owner |
|-------------|--------|----------|--------|-------|
| [.NET Migration](plan/0000-dotnet-migration.md) | ✅ Complete | 🔴 HIGH | 2–3 days | Framework Lead |
| [Paket → NuGet](plan/0001-paket-to-nuget.md) | ✅ Complete | 🔴 HIGH | 1–2 days | Dependency Manager |
| [Package Updates](plan/0002-package-updates.md) | ✅ Complete | 🟡 MEDIUM | 1 day | Maintainer |
| [Code Style Guide](plan/0003-code-style-guide.md) | ✅ Complete | 🟡 MEDIUM | 0.5 day | Tech Lead |
| [String Localization](plan/0004-string-localization.md) | ✅ Complete | 🟡 MEDIUM | 1–2 days | UI/UX Lead |
| [Testing Infrastructure](plan/0005-testing-infrastructure.md) | ✅ Complete | 🟡 MEDIUM | 2–3 days | QA/CI Lead |

---

## Execution Sequence

**Locked order** (dependencies enforced):

1. **[.NET Migration](plan/0000-dotnet-migration.md)** → Unblock framework-dependent work
2. **[Paket → NuGet](plan/0001-paket-to-nuget.md)** → Stabilize dependency resolution
3. **[Package Updates](plan/0002-package-updates.md)** → Modernize transitive dependencies
4. **[Code Style Guide](plan/0003-code-style-guide.md)** → Establish quality baseline (parallel with 0004)
   - *Coordination note: Analyzer rules (0003) integrate into CI later (Plan 0005); non-blocking with 0004*
5. **[String Localization](plan/0004-string-localization.md)** → Improve i18n (parallel with 0003)
   - *Coordination note: String naming aligns with Code Style Guide; asynchronous sync post-implementation*
6. **[Testing Infrastructure](plan/0005-testing-infrastructure.md)** → Add safety net (after stabilization)

---

## Key Milestones

- **Phase 1 (Stabilization)**: Complete net5 → net8 migration, Paket → NuGet transition, package bumps
  - **Trigger for Phase 2**: All projects build without errors; manual testing confirms feature parity
- **Phase 2 (Quality)**: Document code style, localize remaining strings, establish test suite
  - **Trigger for Release**: 70%+ code coverage, all CI checks pass, manual smoke test succeeds

---

## Current State Summary

- **Framework**: ✅ .NET 10.0 LTS (net10.0 TFM); migrated from EOL net5.0; build succeeds
- **Dependency Manager**: ✅ NuGet with Directory.Packages.props (Central Package Management); Paket removed
- **Dependencies**: ✅ Updated to latest stable versions; FSharp.Data 4.2.9, Spectre.Console 0.49.1, SharpZipLib 1.4.2
- **Code Style**: ✅ Formal STYLE_GUIDE.md created; XML doc comments added to Data.fs and Manga.fs
- **Localization**: ✅ All ~40 hard-coded UI strings migrated to Strings.resx; centralized i18n infrastructure in place
- **Testing**: ✅ xUnit test suite with 14 passing tests; automated test infrastructure operational; manual .fsx scripts archived

---

## Tracking

- Each plan file (`docs/plan/NN-*.md`) is independently responsible for its goal, blockers, and status
- This master todo aggregates status across all work streams
- **Update cadence**: After each plan phase completion, update corresponding entry above

