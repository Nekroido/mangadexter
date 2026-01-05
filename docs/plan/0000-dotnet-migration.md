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
| **Current TFM** | `net10.0` (defined in `App.fsproj`) |
| **SDK version** | 10.0 (from `global.json`) |
| **Artifact folders** | `bin/` contains net10.0 artifacts (cleaned) |
| **Paket lock** | Removed; no longer used |
| **Dependencies** | FSharp.Core 10.x (from SDK), FSharp.Data 4.2.9, others updated for net10.0 compatibility |
| **Target SDK** | .NET 10 SDK in use; builds succeed |

---

## Analysis & Discovery

- [x] Confirmed net10.0 compiled artifacts exist
- [x] Verified Paket lock removal
- [x] Analyzed dependency compatibility with net10.0 (FSharp.Core, transitive dependencies)

## Steps
- [x] Update `global.json` — Set `"version": "10.0"`; removed `rollForward: latestMajor`
- [x] Update `src/App/App.fsproj` — Change `<TargetFramework>net5.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`
- [x] Clean build artifacts — Delete `bin/` and `obj/` directories
- [x] Update Paket lock — Removed Paket artifacts (`paket.dependencies`, `paket.lock`, `paket.references`, `.paket/` folder)
- [x] Build and test
  - [x] `dotnet build src/App/App.fsproj`
  - [x] `dotnet run --project src/App/App.fsproj` (manual smoke test)
- [x] Validate dependent modules — Check if other projects in solution require migration

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| Paket lock tied to net5.0 | Dependency resolution may fail or pull mismatched transitive versions | Resolved by removing Paket artifacts |
| FSharp.Core version mismatch | net10.0 SDK ships FSharp.Core 10.x; Paket currently pins 6.0.5 | Resolved by removing Paket and using SDK version |
| Third-party lib incompatibility (AsyncSeq, DotNetZip) | May not build or have runtime issues with net10.0 | Resolved in Package Updates plan; test manually |

---

## Responsible Agent

**Framework Lead** — ownership of .NET SDK version, global.json, project file structure, and cross-project compatibility.

---

## Success Criteria

- ✅ `App.fsproj` targets `net10.0` exclusively
- ✅ `dotnet build` succeeds with no errors
- ✅ Console app runs and completes a search → select → download flow without crashes
- ✅ No compiler warnings related to deprecated APIs or compatibility
- ✅ Paket artifacts removed from source tree
