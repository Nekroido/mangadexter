# Mangadexter Project Roadmap

Master todo for Mangadexter F# console application. Six linked work streams covering framework modernization, dependency management, code quality, testing, and documentation.

---

## Overview

| Work Stream | Status | Priority | Effort | Owner |
|-------------|--------|----------|--------|-------|
| [.NET Migration](plan/0000-dotnet-migration.md) | Not Started | 🔴 HIGH | 2–3 days | Framework Lead |
| [Paket → NuGet](plan/0001-paket-to-nuget.md) | Not Started | 🔴 HIGH | 1–2 days | Dependency Manager |
| [Package Updates](plan/0002-package-updates.md) | Not Started | 🟡 MEDIUM | 1 day | Maintainer |
| [Code Style Guide](plan/0003-code-style-guide.md) | Not Started | 🟡 MEDIUM | 0.5 day | Tech Lead |
| [String Localization](plan/0004-string-localization.md) | Not Started | 🟡 MEDIUM | 1–2 days | UI/UX Lead |
| [Testing Infrastructure](plan/0005-testing-infrastructure.md) | Not Started | 🟡 MEDIUM | 2–3 days | QA/CI Lead |

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

- **Framework**: .NET 5.0 (EOL Nov 2022); SDK allows net6.0+; net7.0/8.0/9.0 artifacts present but stale; target net10.0
- **Dependency Manager**: Paket (lock restricted to net5.0); ~7 dependencies, several 2+ years outdated
- **Testing**: Manual .fsx scripts only; no automated test suite or CI pipeline
- **Documentation**: Minimal README; copilot-instructions.md excellent but external; no in-code docs
- **Code Style**: Consistent formatting (.editorconfig enforced); no formal style guide or analyzer rules
- **Localization**: Partial (Strings.resx exists); ~45 hard-coded UI strings scattered across Pages

---

## Tracking

- Each plan file (`docs/plan/NN-*.md`) is independently responsible for its goal, blockers, and status
- This master todo aggregates status across all work streams
- **Update cadence**: After each plan phase completion, update corresponding entry above

