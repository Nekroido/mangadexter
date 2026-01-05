# Paket → NuGet Transition

## Value

- **Ecosystem alignment**: NuGet is .NET standard; reduces cognitive overhead vs. dual-system (Paket + NuGet for transitive deps)
- **Dependency resolution**: NuGet Central Package Management (CPM) simplifies version pinning; net10.0 lock files more stable
- **Maintenance**: NuGet tooling better integrated in VS/Rider; less manual restore/update ceremony
- **Unblock package updates**: NuGet native support for latest package versions and transitive dependency ranges

---

## Goal

Replace Paket dependency manager with NuGet. Migrate `paket.dependencies` / `paket.lock` to `Directory.Packages.props` and individual project `*.csproj/*.fsproj` package references.

---

## Current State

| Aspect | Details |
|--------|---------|
| **Current DM** | NuGet (Central Package Management) |
| **Lock scope** | Centralized in `Directory.Packages.props` |
| **Restore flow** | `dotnet restore` is standard |
| **Dependencies** | 7 primary (FSharp.Data, Spectre.Console, DotNetZip, etc.); transitive lock is simplified |
| **Project refs** | `src/App/App.fsproj` contains PackageReference entries (no versions) |

---

## Steps

### Already Taken
- [x] Inventoried all direct dependencies in `paket.references`
- [x] Identified transitive dependency chain and compatibility constraints
- [x] Confirmed .NET 10 migration precedes this step (prerequisite)

## Dependencies

**Execution after**: Plan 0000 (.NET Migration)

## Steps
- [x] Create `Directory.Packages.props` — Define all direct + key transitive package versions
  - [x] Reference: `src/Directory.Packages.props` at repo root
  - [x] Set `<ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>`
- [x] Update `src/App/App.fsproj` — Remove Paket imports, add NuGet PackageReference entries (no versions)
- [x] Update other projects (if needed) — Apply same pattern to Core and Gui projects
- [x] Delete Paket artifacts — Remove `paket.dependencies`, `paket.lock`, `paket.references`, `.paket/` folder
- [x] Restore and build
  - [x] `dotnet nuget locals all --clear` (optional)
  - [x] `dotnet restore src/App/App.fsproj`
  - [x] `dotnet build src/App/App.fsproj`
- [x] Validate transitive dependency tree
  - [x] `dotnet list src/App/App.fsproj package`
  - [x] Run manual smoke tests (search, download, archive)

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| Paket transitive lock is complex | Manual migration may miss nested constraints | Resolved by using Directory.Packages.props |
| Legacy Paket-specific syntax in .fsproj | Build fails if imports not fully removed | Resolved by removing all Paket imports in App.fsproj |
| Downtime during migration | Build breaks if deps not fully migrated | Resolved by testing in isolated branch |

---

## Responsible Agent

**Dependency Manager** — ownership of package resolution, version pinning, transitive dependency tree, and build artifact stability.

---

## Success Criteria

- ✅ `Directory.Packages.props` defines all packages with versions
- ✅ `App.fsproj` contains PackageReference entries (no versions)
- ✅ `dotnet restore` and `dotnet build` succeed without errors
- ✅ No Paket artifacts remain in source tree (paket.lock, paket.references, Paket.Restore.targets)
- ✅ Console app runs and executes full search → select → download flow
- ✅ `dotnet list` shows all expected transitive dependencies resolved
