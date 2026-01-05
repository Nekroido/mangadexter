# Testing Infrastructure & CI Pipeline

## Value

- **Regression prevention**: Automated tests catch breaking changes before release
- **Safety net**: Confidence to refactor and upgrade dependencies without manual testing overhead
- **CI/CD readiness**: Enable GitHub Actions (or similar) to validate builds on every commit
- **Developer velocity**: Faster feedback loop; merge confidence increases with test coverage

---

## Goal

Establish automated test suite (xUnit or NUnit). Migrate manual .fsx test scripts to proper unit/integration tests. Set up GitHub Actions workflow for CI.

---

## Current State

| Aspect | Details |
|--------|---------|
| **Test infrastructure** | ✅ xUnit test project created and configured |
| **Manual testing** | ✅ Manual .fsx test scripts archived |
| **Test framework** | ✅ xUnit framework installed and configured |
| **CI/CD pipeline** | ✅ GitHub Actions workflow configured |
| **Build artifacts** | ✅ Release binaries present in bin/Release/net10.0/win-x64/ |
| **Coverage** | ✅ 70%+ coverage on core logic (Data, Manga, File) |

---

## Steps

### Already Taken
- [x] Inventoried existing .fsx test scripts (test.fsx, test2.fsx, test3.fsx)
- [x] Confirmed no test framework or CI infrastructure existed
- [x] Identified prerequisites: .NET migration, NuGet transition, package updates

## Prerequisites (must complete before 0005 starts)

- [x] Plan 0000 (.NET Migration) — Status: Complete
- [x] Plan 0001 (Paket → NuGet) — Status: Complete
- [x] Plan 0002 (Package Updates) — Status: Complete

**Critical path**: All three prerequisites have been completed before proceeding with test infrastructure setup.

## Steps
- [x] Create test project structure
  - [x] Add `src/App.Tests/App.Tests.fsproj` (xUnit-based)
  - [x] Reference App.fsproj as a project dependency
  - [x] Add xUnit and xUnit.Runner.VisualStudio to Directory.Packages.props
- [x] Migrate manual test scripts
  - [x] Analyze test.fsx, test2.fsx, test3.fsx for test scenarios
  - [x] Create test modules: Tests.Data.fs, Tests.Manga.fs, Tests.File.fs
- [x] Write test cases
  - [x] Target: Isolate testable functions (pure functions, Result-based error handling)
  - [x] Scope: 70%+ coverage on core logic (Data, Manga, File)
  - [x] Examples: `Data.searchManga`, `Manga.formatTitle`, `File.cbzBuilder`
- [x] Configure GitHub Actions CI workflow
  - [x] Create `.github/workflows/build-and-test.yml`
  - [x] Steps: Restore, build, test, publish results
  - [x] Triggers: On push/PR to main/develop
- [x] Run tests locally and in CI
  - [x] `dotnet test src/App.Tests/App.Tests.fsproj`
  - [x] Verify all tests pass on main branch
  - [x] Validate CI workflow on PR
- [x] Document testing practices (optional)
  - [x] Add section to docs/STYLE_GUIDE.md: test naming, mocking patterns, test organization

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| Tight coupling between modules and external I/O | Hard to test without real network/file calls | Resolved by refactoring core logic into pure functions; using dependency injection or mock interfaces for Http/File modules |
| No existing test patterns in codebase | New contributors unfamiliar with test structure | Resolved by documenting test examples and patterns in copilot-instructions.md or STYLE_GUIDE.md |
| CI pipeline setup complexity | GitHub Actions workflow may have syntax errors | Resolved by starting with basic build + test; iterating on coverage/artifacts |

---

## Responsible Agent

**QA/CI Lead** — ownership of test project structure, test case design, GitHub Actions workflow, and CI/CD automation. Coordinate with Framework/Maintainer leads on ensuring all dependencies and code changes are tested.

---

## Success Criteria

- ✅ `src/App.Tests/App.Tests.fsproj` created with xUnit framework
- ✅ Test modules created for Data, Manga, File (minimum 20 test cases)
- ✅ `dotnet test` runs successfully; all tests pass
- ✅ `.github/workflows/build-and-test.yml` created; CI runs on push/PR
- ✅ Coverage for core logic ≥70% (Core Data, Manga, File modules)
- ✅ Manual .fsx test scripts migrated or archived with notes
