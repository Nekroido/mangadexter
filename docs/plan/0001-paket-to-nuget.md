# Paket → NuGet Transition

## Value

- **Ecosystem alignment**: NuGet is .NET standard; reduces cognitive overhead vs. dual-system (Paket + NuGet for transitive deps)
- **Dependency resolution**: NuGet Central Package Management (CPM) simplifies version pinning; net8.0 lock files more stable
- **Maintenance**: NuGet tooling better integrated in VS/Rider; less manual restore/update ceremony
- **Unblock package updates**: NuGet native support for latest package versions and transitive dependency ranges

---

## Goal

Replace Paket dependency manager with NuGet. Migrate `paket.dependencies` / `paket.lock` to `Directory.Packages.props` and individual project `*.csproj/*.fsproj` package references.

---

## Current State

| Aspect | Details |
|--------|---------|
| **Current DM** | Paket v6+ (lock file: `paket.lock`, config: `paket.dependencies`) |
| **Lock scope** | Restricted to `net5.0` context; misaligned with net10.0 target |
| **Restore flow** | `.\.paket\paket.exe restore` required before build |
| **Dependencies** | 7 primary (FSharp.Data, Spectre.Console, DotNetZip, etc.); transitive lock is complex |
| **Project refs** | `src/App/paket.references` lists direct deps; MSBuild import in .fsproj adds Paket.Restore.targets |

---

## Steps

### Already Taken
- [x] Inventoried all direct dependencies in `paket.references`
- [x] Identified transitive dependency chain and compatibility constraints
- [x] Confirmed .NET 10 migration precedes this step (prerequisite)

## Dependencies

**Execution after**: Plan 0000 (.NET Migration)

## Steps
- [ ] Create `Directory.Packages.props` — Define all direct + key transitive package versions
  - [ ] Reference: `src/Directory.Packages.props` at repo root
  - [ ] Set `<ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>`
- [ ] Update `src/App/App.fsproj` — Remove Paket imports, add NuGet PackageReference entries (no versions)
- [ ] Update other projects (if needed) — Apply same pattern to Core and Gui projects
- [ ] Delete Paket artifacts — Remove `paket.dependencies`, `paket.lock`, `paket.references`, `.paket/` folder
- [ ] Restore and build
  - [ ] `dotnet nuget locals all --clear` (optional)
  - [ ] `dotnet restore src/App/App.fsproj`
  - [ ] `dotnet build src/App/App.fsproj`
- [ ] Validate transitive dependency tree
  - [ ] `dotnet list src/App/App.fsproj package`
  - [ ] Run manual smoke tests (search, download, archive)

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| Paket transitive lock is complex | Manual migration may miss nested constraints | Use `paket show-installed-packages` to generate baseline; cross-check against Directory.Packages.props |
| Legacy Paket-specific syntax in .fsproj | Build fails if imports not fully removed | Audit all Paket imports in App.fsproj and related files |
| Downtime during migration | Build breaks if deps not fully migrated | Test in isolated branch; no commits until full build pass |

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

