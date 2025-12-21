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
| **Test infrastructure** | ❌ None; no test projects or frameworks configured |
| **Manual testing** | ✅ test.fsx, test2.fsx, test3.fsx present; ad-hoc validation only |
| **Test framework** | ❌ None (xUnit, NUnit, or Expecto not installed) |
| **CI/CD pipeline** | ❌ No GitHub Actions, AppVeyor, or other CI automation |
| **Build artifacts** | Release binaries present in bin/Release/net{7,8,9}.0/win-x64/ but no publish workflow |
| **Coverage** | Unknown (no instrumentation tools configured) |

---

## Steps

### Already Taken
- [x] Inventoried existing .fsx test scripts (test.fsx, test2.fsx, test3.fsx)
- [x] Confirmed no test framework or CI infrastructure exists
- [x] Identified prerequisites: .NET migration, NuGet transition, package updates

## Prerequisites (must complete before 0005 starts)

- [ ] Plan 0000 (.NET Migration) — Status: Not Started
- [ ] Plan 0001 (Paket → NuGet) — Status: Not Started
- [ ] Plan 0002 (Package Updates) — Status: Not Started

**Critical path**: All three prerequisites must reach completion before proceeding with test infrastructure setup.

## Steps
- [ ] Create test project structure
  - [ ] Add `src/App.Tests/App.Tests.fsproj` (xUnit-based)
  - [ ] Reference App.fsproj as a project dependency
  - [ ] Add xUnit and xUnit.Runner.VisualStudio to Directory.Packages.props
- [ ] Migrate manual test scripts
  - [ ] Analyze test.fsx, test2.fsx, test3.fsx for test scenarios
  - [ ] Create test modules: Tests.Data.fs, Tests.Manga.fs, Tests.File.fs
- [ ] Write test cases
  - [ ] Target: Isolate testable functions (pure functions, Result-based error handling)
  - [ ] Scope: 70%+ coverage on core logic (Data, Manga, File)
  - [ ] Examples: `Data.searchManga`, `Manga.formatTitle`, `File.cbzBuilder`
- [ ] Configure GitHub Actions CI workflow
  - [ ] Create `.github/workflows/build-and-test.yml`
  - [ ] Steps: Restore, build, test, publish results
  - [ ] Triggers: On push/PR to main/develop
- [ ] Run tests locally and in CI
  - [ ] `dotnet test src/App.Tests/App.Tests.fsproj`
  - [ ] Verify all tests pass on main branch
  - [ ] Validate CI workflow on PR
- [ ] Document testing practices (optional)
  - [ ] Add section to docs/STYLE_GUIDE.md: test naming, mocking patterns, test organization

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| Tight coupling between modules and external I/O | Hard to test without real network/file calls | Refactor core logic into pure functions; use dependency injection or mock interfaces for Http/File modules |
| No existing test patterns in codebase | New contributors unfamiliar with test structure | Document test examples and patterns in copilot-instructions.md or STYLE_GUIDE.md |
| CI pipeline setup complexity | GitHub Actions workflow may have syntax errors | Start with basic build + test; iterate on coverage/artifacts |

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

