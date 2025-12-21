# .NET 5.0 → 10.0 LTS Migration

## Value

- **Security**: End-of-life framework (Nov 2022) no longer receives patches; migrate to current stable (net10.0)
- **Compatibility**: Latest SDK tooling, analyzers, and language features (F# 8+); align with modern .NET ecosystem
- **Performance**: net10.0 includes performance improvements and better AOT compilation support
- **Unblock dependent work**: Framework stabilization is prerequisite for Paket → NuGet transition and package updates

---

## Goal

Update `src/App/App.fsproj` to target `net10.0`. Verify all features work without regression.

---

## Current State

| Aspect | Details |
|--------|---------|
| **Current TFM** | `net5.0` (defined in `App.fsproj`) |
| **SDK version** | 6.0 (from `global.json`) with `rollForward: latestMajor` |
| **Artifact folders** | `bin/` contains net5.0, net7.0, net8.0, net9.0 folders (stale multi-targeting) |
| **Paket lock** | `paket.lock` is scoped to `net5.0`; net6+ dependency resolution may be inconsistent |
| **Dependencies** | FSharp.Core 6.0.5, FSharp.Data 4.2.9, others pinned to net5.0 compatibility |
| **Target SDK** | No .NET 10 SDK in use; builds succeed against 6.0 due to `rollForward` |

---

## Analysis & Discovery

- [x] Confirmed net10.0 and net9.0 compiled artifacts exist (stale)
- [x] Identified Paket lock as blocker for net6+ dependency resolution
- [x] Analyzed dependency compatibility with net10.0 (FSharp.Core, transitive dependencies)

## Steps
- [ ] Update `global.json` — Set `"version": "10.0.x"`; remove or update `rollForward: latestMajor`
- [ ] Update `src/App/App.fsproj` — Change `<TargetFramework>net5.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`
- [ ] Clean build artifacts — Delete `bin/` and `obj/` directories
- [ ] Update Paket lock — Edit `paket.dependencies` to remove framework constraints (or set to `framework: net10.0`)
  - [ ] Run `.\.paket\paket.exe update` to generate net10.0-compatible `paket.lock`
- [ ] Build and test
  - [ ] `dotnet build src/App/App.fsproj`
  - [ ] `dotnet run --project src/App/App.fsproj` (manual smoke test)
- [ ] Validate dependent modules — Check if other projects in solution require migration

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| Paket lock tied to net5.0 | Dependency resolution may fail or pull mismatched transitive versions | Addressed in step 4; Paket → NuGet plan handles permanent solution |
| FSharp.Core version mismatch | net10.0 SDK ships FSharp.Core 10.x; Paket currently pins 6.0.5 | Build-time check: Paket lock regeneration (step 4) resolves version automatically. No external dependency on Plan 0002. |
| Third-party lib incompatibility (AsyncSeq, DotNetZip) | May not build or have runtime issues with net10.0 | Addressed in Package Updates plan; test manually |

---

## Responsible Agent

**Framework Lead** — ownership of .NET SDK version, global.json, project file structure, and cross-project compatibility.

---

## Success Criteria

- ✅ `App.fsproj` targets `net10.0` exclusively
- ✅ `dotnet build` succeeds with no errors
- ✅ Console app runs and completes a search → select → download flow without crashes
- ✅ No compiler warnings related to deprecated APIs or compatibility
- ✅ Paket lock updated for net10.0 context

